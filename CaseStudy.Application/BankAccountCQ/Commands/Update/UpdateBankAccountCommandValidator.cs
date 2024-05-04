using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Update;

public class UpdateBankAccountCommandValidator : CommandValidator<UpdateBankAccountCommand>
{
    public UpdateBankAccountCommandValidator()
    {
        RuleFor(e => e.Name).Length(3, 100);
        RuleFor(e => e.Id).GreaterThan(0);

    }
}