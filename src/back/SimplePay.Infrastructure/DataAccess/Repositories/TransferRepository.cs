using SimplePay.Domain.Repositories.Transfer;

namespace SimplePay.Infrastructure.DataAccess.Repositories;

internal sealed class TransferRepository : ITransferWriter
{
    private readonly SimplePayDbContext _dbContext;

    public TransferRepository(SimplePayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Domain.Entities.Transfer transfer, CancellationToken cancellationToken) =>
        await _dbContext.Transfers.AddAsync(transfer, cancellationToken);
}
