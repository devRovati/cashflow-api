using CashFlowApi.Application.DTOs.Transactions;
using FluentValidation;

namespace CashFlowApi.Application.Validators;

public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
{
    public TransactionRequestValidator()
    {
        RuleFor(x => x.Amount)
    .       GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

        RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.")
                .Must(date => date > DateTime.MinValue).WithMessage("Invalid payment date.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        RuleFor(x => x.TransactionType)
            .IsInEnum().WithMessage("Invalid transaction type.");
    }
}
