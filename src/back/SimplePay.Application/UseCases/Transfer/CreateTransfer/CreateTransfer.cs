using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;
using SimplePay.Domain.Enums;
using SimplePay.Domain.ExternalServices;
using SimplePay.Domain.Repositories;
using SimplePay.Domain.Repositories.Transfer;
using SimplePay.Domain.Repositories.User;
using SimplePay.Domain.Repositories.Wallet;
using SimplePay.Exception;
using SimplePay.Exception.ExceptionsBase;

namespace SimplePay.Application.UseCases.Transfer.CreateTransfer;

public class CreateTransfer(
    IUserReader userReader,
    IWalletReader walletReader,
    ITransferWriter transferWriter,
    IAuthorizerService authorizerService,
    INotificationService notificationService,
    IUnitOfWork unitOfWork) : ICreateTransfer
{
    private readonly IUserReader _userReader = userReader;
    private readonly IWalletReader _walletReader = walletReader;
    private readonly ITransferWriter _transferWriter = transferWriter;
    private readonly IAuthorizerService _authorizerService = authorizerService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<TransferResponse> Execute(TransferRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        var payer = await _userReader.GetById(request.Payer, cancellationToken)
            ?? throw new NotFoundException(MessagesExceptionResource.USER_NOT_FOUND_VALIDATION);

        var payee = await _userReader.GetById(request.Payee, cancellationToken)
            ?? throw new NotFoundException(MessagesExceptionResource.USER_NOT_FOUND_VALIDATION);

        if (payer.Type == UserType.Shopkeeper)
            throw new ValidationException([MessagesExceptionResource.SHOPKEEPER_CANNOT_SEND_VALIDATION]);

        var payerWallet = await _walletReader.GetByUserId(payer.Id, cancellationToken)
            ?? throw new NotFoundException(MessagesExceptionResource.USER_NOT_FOUND_VALIDATION);

        var payeeWallet = await _walletReader.GetByUserId(payee.Id, cancellationToken)
            ?? throw new NotFoundException(MessagesExceptionResource.USER_NOT_FOUND_VALIDATION);

        if (payerWallet.Balance < request.Value)
            throw new ValidationException([MessagesExceptionResource.INSUFFICIENT_BALANCE_VALIDATION]);

        var isAuthorized = await _authorizerService.Authorize(cancellationToken);

        if (isAuthorized == false)
            throw new ValidationException([MessagesExceptionResource.TRANSFER_NOT_AUTHORIZED_VALIDATION]);

        payerWallet.Balance -= request.Value;
        payerWallet.Version++;

        payeeWallet.Balance += request.Value;
        payeeWallet.Version++;

        var transfer = new Domain.Entities.Transfer
        {
            PayerId = payer.Id,
            PayeeId = payee.Id,
            Amount = request.Value,
            CreatedAt = DateTime.UtcNow
        };

        await _transferWriter.Add(transfer, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        try
        {
            await _notificationService.Notify(
                payee.Email,
                $"Você recebeu uma transferência de {request.Value:C}",
                cancellationToken);
        }
        catch (System.Exception)
        {
            //
        }

        return new TransferResponse
        {
            Id = transfer.Id,
            Payer = transfer.PayerId,
            Payee = transfer.PayeeId,
            Value = transfer.Amount,
            CreatedAt = transfer.CreatedAt
        };
    }

    private static void Validate(TransferRequest request)
    {
        var validator = new TransferValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}