using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

namespace CaseStudy.Application.ContactPersonCQ.Queries.Get;

public class GetContactPersonByIdQueryHandler : IQueryHandler<GetContactPersonByIdQuery, ContactPersonDTO>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.ContactPerson> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetContactPersonByIdQueryHandler(IRepository<Domain.VendorAggregate.Entities.ContactPerson> bankRepository, IUnitOfWork unitOfWork)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
    }
    public Task<Result<ContactPersonDTO>> Handle(GetContactPersonByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}