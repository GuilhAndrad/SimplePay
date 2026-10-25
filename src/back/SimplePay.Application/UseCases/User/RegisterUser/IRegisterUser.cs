using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.Application.UseCases.User.RegisterUser;

public interface IRegisterUser
{
    Task<RegisteredUserResponse> Execute(RegisterUserRequest request, CancellationToken cancellationToken);
}
