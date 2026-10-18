using SimplePay.Domain.Repositories;

namespace SimplePay.Infrastructure.DataAccess;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly SimplePayDbContext _dbContext;

    public UnitOfWork(SimplePayDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit(CancellationToken cancellationToken) =>
            await _dbContext.SaveChangesAsync(cancellationToken);
}
