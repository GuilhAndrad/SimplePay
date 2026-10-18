namespace SimplePay.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}
