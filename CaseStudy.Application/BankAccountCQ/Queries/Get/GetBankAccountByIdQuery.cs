using HamedStack.CQRS;

namespace CaseStudy.Application.BankAccountCQ.Queries.Get;

public class GetBankAccountByIdQuery : IQuery<BankAccountDTO>
{
    public int Id { get; set; }
    public int VendorId { get; set; }
}