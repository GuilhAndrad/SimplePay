namespace SimplePay.Domain.Repositories.Wallet;

public interface IWalletReader
{
    Task<Entities.Wallet?> GetByUserId(int userId, CancellationToken cancellationToken);
}