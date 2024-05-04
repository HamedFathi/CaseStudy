using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Queries.List;

public class ListVendorQueryValidator : QueryValidator<ListVendorQuery, IEnumerable<VendorDTO>>
{
    public ListVendorQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .When(x => x.PageIndex != null);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .When(x => x.PageSize != null);

    }
}