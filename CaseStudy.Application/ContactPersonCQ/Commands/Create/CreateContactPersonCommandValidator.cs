using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Create;

public class CreateContactPersonCommandValidator : CommandValidator<CreateContactPersonCommand, int>
{
    public CreateContactPersonCommandValidator()
    {
        RuleFor(e => e.FirstName).Length(3, 100);
        RuleFor(e => e.LastName).Length(3, 100);
    }
}