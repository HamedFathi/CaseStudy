using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Create;

public class CreateBankAccountCommandValidator : CommandValidator<CreateBankAccountCommand, int>
{
    public CreateBankAccountCommandValidator()
    {
        RuleFor(e => e.Name).Length(3, 100);
    }
}