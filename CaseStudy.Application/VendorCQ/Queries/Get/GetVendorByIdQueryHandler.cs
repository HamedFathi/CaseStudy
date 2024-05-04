using CaseStudy.Domain.VendorAggregate;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

namespace CaseStudy.Application.VendorCQ.Queries.Get;

public class GetVendorByIdQueryHandler : IQueryHandler<GetVendorByIdQuery, VendorDTO>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetVendorByIdQueryHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
    }
    public Task<Result<VendorDTO>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}