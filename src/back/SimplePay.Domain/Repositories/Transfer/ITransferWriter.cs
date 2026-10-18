namespace SimplePay.Domain.Repositories.Transfer;

public interface ITransferWriter
{
    Task Add(Entities.Transfer transfer, CancellationToken cancellationToken);
}