using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Delete;

public class DeleteVendorCommandValidator : CommandValidator<DeleteVendorCommand>
{
    public DeleteVendorCommandValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
    }
}