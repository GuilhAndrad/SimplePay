namespace SimplePay.Domain.Repositories.User;

public interface IUserWriter
{
    Task Add(Entities.User user, CancellationToken cancellationToken);
}
