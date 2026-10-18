namespace SimplePay.Domain.Repositories.Wallet;

public interface IWalletWriter
{
    Task Add(Entities.Wallet wallet, CancellationToken cancellationToken);
}
