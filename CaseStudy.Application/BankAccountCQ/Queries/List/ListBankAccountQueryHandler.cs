using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

namespace CaseStudy.Application.BankAccountCQ.Queries.List;

public class ListBankAccountQueryHandler : IQueryHandler<ListBankAccountQuery, IEnumerable<BankAcountDTO>>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.BankAccount> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ListBankAccountQueryHandler(IRepository<Domain.VendorAggregate.Entities.BankAccount> bankRepository, IUnitOfWork unitOfWork)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Result<IEnumerable<BankAcountDTO>>> Handle(ListBankAccountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}