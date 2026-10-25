using FluentValidation;
using SimplePay.Communication.Requests;
using SimplePay.Exception;

namespace SimplePay.Application.UseCases.Wallet.Deposit;

public class DepositValidator : AbstractValidator<DepositRequest>
{
    public DepositValidator()
    {
        RuleFor(deposit => deposit.Value)
            .GreaterThan(0).WithMessage(MessagesExceptionResource.DEPOSIT_VALUE_INVALID_VALIDATION);
    }
}