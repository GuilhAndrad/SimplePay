using FluentValidation;
using SimplePay.Application.Validators;
using SimplePay.Communication.Requests;
using SimplePay.Exception;

namespace SimplePay.Application.UseCases.User.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage(MessagesExceptionResource.FULL_NAME_REQUIRED_VALIDATION);

        RuleFor(user => user.Document)
            .NotEmpty().WithMessage(MessagesExceptionResource.DOCUMENT_REQUIRED_VALIDATION);

        RuleFor(user => user)
            .Must(user => DocumentValidator.IsValid(user.Document, user.Type))
            .WithMessage(MessagesExceptionResource.DOCUMENT_INVALID_VALIDATION)
            .When(user => string.IsNullOrWhiteSpace(user.Document) == false);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(MessagesExceptionResource.EMAIL_REQUIRED_VALIDATION)
            .EmailAddress().WithMessage(MessagesExceptionResource.EMAIL_INVALID_VALIDATION);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(MessagesExceptionResource.PASSWORD_REQUIRED_VALIDATION)
            .MinimumLength(8).WithMessage(MessagesExceptionResource.PASSWORD_MIN_LENGTH_VALIDATION);
    }
}