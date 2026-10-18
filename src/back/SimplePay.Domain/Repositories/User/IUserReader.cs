namespace SimplePay.Domain.Repositories.User;

public interface IUserReader
{
    Task<Entities.User?> GetById(int id, CancellationToken cancellationToken);
    Task<bool> ExistActiveUserWithDocument(string document, CancellationToken cancellationToken);
    Task<bool> ExistActiveUserWithEmail(string email, CancellationToken cancellationToken);
}