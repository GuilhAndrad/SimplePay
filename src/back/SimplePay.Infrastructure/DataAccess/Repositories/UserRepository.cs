using Microsoft.EntityFrameworkCore;
using SimplePay.Domain.Repositories.User;

namespace SimplePay.Infrastructure.DataAccess.Repositories;

internal sealed class UserRepository : IUserReader, IUserWriter
{
    private readonly SimplePayDbContext _dbContext;

    public UserRepository(SimplePayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Domain.Entities.User user, CancellationToken cancellationToken) =>
        await _dbContext.AddAsync(user, cancellationToken);

    public async Task<Domain.Entities.User?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(user => user.Active && user.Id == id, cancellationToken);
    }

    public async Task<bool> ExistActiveUserWithDocument(string document, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(user => user.Active && user.Document == document, cancellationToken);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(user => user.Active && user.Email == email, cancellationToken);
    }
}
