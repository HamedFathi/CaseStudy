using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Update;

public class UpdateContactPersonCommandValidator : CommandValidator<UpdateContactPersonCommand>
{
    public UpdateContactPersonCommandValidator()
    {
        RuleFor(e => e.FirstName).Length(3, 100);
        RuleFor(e => e.LastName).Length(3, 100);
    }
}