using FluentValidation;
using SimplePay.Communication.Requests;
using SimplePay.Exception;

namespace SimplePay.Application.UseCases.Transfer.CreateTransfer;

public class TransferValidator : AbstractValidator<TransferRequest>
{
    public TransferValidator()
    {
        RuleFor(transfer => transfer.Value)
            .GreaterThan(0).WithMessage(MessagesExceptionResource.TRANSFER_VALUE_INVALID_VALIDATION);

        RuleFor(transfer => transfer)
            .Must(transfer => transfer.Payer != transfer.Payee)
            .WithMessage(MessagesExceptionResource.TRANSFER_SAME_USER_VALIDATION);
    }
}