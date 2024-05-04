using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

namespace CaseStudy.Application.ContactPersonCQ.Queries.List;

public class ListContactPersonQueryHandler : IQueryHandler<ListContactPersonQuery, IEnumerable<ContactPersonDTO>>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.ContactPerson> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ListContactPersonQueryHandler(IRepository<Domain.VendorAggregate.Entities.ContactPerson> bankRepository, IUnitOfWork unitOfWork)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
    }
    public Task<Result<IEnumerable<ContactPersonDTO>>> Handle(ListContactPersonQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}