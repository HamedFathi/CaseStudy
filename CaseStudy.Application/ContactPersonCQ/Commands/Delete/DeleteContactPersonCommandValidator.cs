using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Delete;

public class DeleteContactPersonCommandValidator : CommandValidator<DeleteContactPersonCommand>
{
    public DeleteContactPersonCommandValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
        RuleFor(e => e.VendorId).GreaterThan(0);
    }
}