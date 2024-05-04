using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

namespace CaseStudy.Application.BankAccountCQ.Queries.Get;

public class GetBankAccountByIdQueryHandler : IQueryHandler<GetBankAccountByIdQuery, BankAcountDTO>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.BankAccount> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetBankAccountByIdQueryHandler(IRepository<Domain.VendorAggregate.Entities.BankAccount> bankRepository, IUnitOfWork unitOfWork)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
    }
    public Task<Result<BankAcountDTO>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}