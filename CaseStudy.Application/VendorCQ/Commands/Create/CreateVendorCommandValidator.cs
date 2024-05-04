using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Create;

public class CreateVendorCommandValidator : CommandValidator<CreateVendorCommand, int>
{
    public CreateVendorCommandValidator()
    {
        RuleFor(e => e.Name).Length(3, 100);
        RuleFor(e => e.Mail).EmailAddress();
    }
}