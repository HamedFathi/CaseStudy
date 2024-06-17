using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Domain.VendorAggregate;
using FluentValidation;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CaseStudy.Application.VendorCQ.Queries.Get;

public class GetVendorByIdQueryHandler : IQueryHandler<GetVendorByIdQuery, VendorDTO>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<GetVendorByIdQuery> _vendorValidator;
    private readonly IDistributedCache _distributedCache;

    public GetVendorByIdQueryHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork, IValidator<GetVendorByIdQuery> vendorValidator, IDistributedCache distributedCache)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
        _vendorValidator = vendorValidator;
        _distributedCache = distributedCache;
    }
    public async Task<Result<VendorDTO>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _vendorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<VendorDTO>(default);
        }

        var cachedData = await _distributedCache.GetObjectAsync<Vendor>(nameof(GetVendorByIdQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<VendorDTO>.Success(MapTo(cachedData));
        }

        var output = await _vendorRepository.GetByIdsAsync(new object[] { request.Id }, cancellationToken);

        if (output == null)
        {
            return (Result<VendorDTO>)Result.Failure("Id not found.");
        }

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        await _distributedCache.SetObjectAsync(nameof(GetVendorByIdQuery), output, entryOptions: options, cancellationToken: cancellationToken);

        return Result<VendorDTO>.Success(MapTo(output));
    }

    private VendorDTO MapTo(Vendor vendor)
    {
        return new VendorDTO()
        {
            Name = vendor.Name,
            Id = vendor.Id,
            Phone = vendor.Phone,
            Mail = vendor.Mail,
            Address1 = vendor.Address.Address1,
            Address2 = vendor.Address.Address2,
            City = vendor.Address.City,
            Country = vendor.Address.Country,
            Name2 = vendor.Name2?.Value,
            Notes = vendor.Notes,
            Zip = vendor.Address.Zip
        };
    }
}