using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Delete;

public class DeleteBankAccountCommandValidator : CommandValidator<DeleteBankAccountCommand>
{
    public DeleteBankAccountCommandValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
    }
}