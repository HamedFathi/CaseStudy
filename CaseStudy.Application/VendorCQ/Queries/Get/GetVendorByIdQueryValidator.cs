using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Queries.Get;

public class GetVendorByIdQueryValidator : QueryValidator<GetVendorByIdQuery, VendorDTO>
{
    public GetVendorByIdQueryValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
    }
}