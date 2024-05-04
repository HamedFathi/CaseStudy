using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Update;

public class UpdateVendorCommandValidator : CommandValidator<UpdateVendorCommand>
{
    public UpdateVendorCommandValidator()
    {
        RuleFor(e => e.Name).Length(3, 100);
        RuleFor(e => e.Mail).EmailAddress();
    }
}