using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Queries.List;

public class ListBankAccountQueryValidator : QueryValidator<ListBankAccountQuery, IEnumerable<BankAccountDTO>>
{
    public ListBankAccountQueryValidator()
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