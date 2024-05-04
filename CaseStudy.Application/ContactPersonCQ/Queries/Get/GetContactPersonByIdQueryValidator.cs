using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Queries.Get;

public class GetContactPersonByIdQueryValidator : QueryValidator<GetContactPersonByIdQuery, ContactPersonDTO>
{
    public GetContactPersonByIdQueryValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
        RuleFor(e => e.VendorId).GreaterThan(0);
    }
}