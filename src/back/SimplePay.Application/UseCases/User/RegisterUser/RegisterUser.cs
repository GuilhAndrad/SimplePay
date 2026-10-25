using Mapster;
using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;
using SimplePay.Domain.Repositories;
using SimplePay.Domain.Repositories.User;
using SimplePay.Domain.Repositories.Wallet;
using SimplePay.Domain.Security;
using SimplePay.Exception;
using SimplePay.Exception.ExceptionsBase;

namespace SimplePay.Application.UseCases.User.RegisterUser;

public class RegisterUser(
    IUserReader userReader,
    IUserWriter userWriter,
    IWalletWriter walletWriter,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IRegisterUser
{
    private readonly IUserReader _userReader = userReader;
    private readonly IUserWriter _userWriter = userWriter;
    private readonly IWalletWriter _walletWriter = walletWriter;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<RegisteredUserResponse> Execute(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        if (await _userReader.ExistActiveUserWithDocument(request.Document, cancellationToken))
            throw new ValidationException([MessagesExceptionResource.DOCUMENT_ALREADY_REGISTERED_VALIDATION]);

        if (await _userReader.ExistActiveUserWithEmail(request.Email, cancellationToken))
            throw new ValidationException([MessagesExceptionResource.EMAIL_ALREADY_REGISTERED_VALIDATION]);

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordHasher.Hash(request.Password);

        await _userWriter.Add(user, cancellationToken);

        var wallet = new Domain.Entities.Wallet { User = user };
        await _walletWriter.Add(wallet, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return new RegisteredUserResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Type = user.Type.Adapt<Communication.Enums.UserType>()
        };
    }

    private static void Validate(RegisterUserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}