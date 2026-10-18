using Microsoft.EntityFrameworkCore;
using SimplePay.Domain.Repositories.Wallet;

namespace SimplePay.Infrastructure.DataAccess.Repositories;

internal sealed class WalletRepository : IWalletReader, IWalletWriter
{
    private readonly SimplePayDbContext _dbContext;

    public WalletRepository(SimplePayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Domain.Entities.Wallet wallet, CancellationToken cancellationToken) =>
            await _dbContext.Wallets.AddAsync(wallet, cancellationToken);

    public async Task<Domain.Entities.Wallet?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Wallets.SingleOrDefaultAsync(wallet => wallet.Active && wallet.UserId == userId, cancellationToken);
    }
}
