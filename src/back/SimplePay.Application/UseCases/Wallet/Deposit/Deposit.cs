using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;
using SimplePay.Domain.Repositories;
using SimplePay.Domain.Repositories.Wallet;
using SimplePay.Exception;
using SimplePay.Exception.ExceptionsBase;

namespace SimplePay.Application.UseCases.Wallet.Deposit;

public class Deposit(IWalletReader walletReader, IUnitOfWork unitOfWork) : IDeposit
{
    private readonly IWalletReader _walletReader = walletReader;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<DepositResponse> Execute(DepositRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        var wallet = await _walletReader.GetByUserId(request.UserId, cancellationToken)
            ?? throw new NotFoundException(MessagesExceptionResource.USER_NOT_FOUND_VALIDATION);

        wallet.Balance += request.Value;
        wallet.Version++;

        await _unitOfWork.Commit(cancellationToken);

        return new DepositResponse
        {
            UserId = request.UserId,
            Balance = wallet.Balance
        };
    }

    private static void Validate(DepositRequest request)
    {
        var validator = new DepositValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}