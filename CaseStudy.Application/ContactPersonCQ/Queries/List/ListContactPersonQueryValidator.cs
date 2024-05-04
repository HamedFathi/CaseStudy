using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Queries.List;

public class ListContactPersonQueryValidator : QueryValidator<ListContactPersonQuery, IEnumerable<ContactPersonDTO>>
{
    public ListContactPersonQueryValidator()
    {
        RuleFor(e => e.VendorId).GreaterThan(0);
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .When(x => x.PageIndex != null);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .When(x => x.PageSize != null);

    }
}