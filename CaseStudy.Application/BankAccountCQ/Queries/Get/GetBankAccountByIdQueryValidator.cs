using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Queries.Get;

public class GetBankAccountByIdQueryValidator : QueryValidator<GetBankAccountByIdQuery, BankAccountDTO>
{
    public GetBankAccountByIdQueryValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
        RuleFor(e => e.VendorId).GreaterThan(0);
    }
}