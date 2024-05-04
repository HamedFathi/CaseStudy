using CaseStudy.Domain.VendorAggregate;
using FluentValidation;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using HamedStack.Paging;
using Microsoft.Extensions.Caching.Distributed;

namespace CaseStudy.Application.VendorCQ.Queries.List;

public class ListVendorQueryHandler : IQueryHandler<ListVendorQuery, IEnumerable<VendorDTO>>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ListVendorQuery> _vendorValidator;
    private readonly IDistributedCache _distributedCache;

    public ListVendorQueryHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork, IValidator<ListVendorQuery> vendorValidator, IDistributedCache distributedCache)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
        _vendorValidator = vendorValidator;
        _distributedCache = distributedCache;
    }
    public async Task<Result<IEnumerable<VendorDTO>>> Handle(ListVendorQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _vendorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return (Result<IEnumerable<VendorDTO>>)validationResult.ToResult();
        }

        var cachedData = await _distributedCache.GetObjectAsync<List<Vendor>>(nameof(ListVendorQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<IEnumerable<VendorDTO>>.Success(MapTo(cachedData));
        }

        var output = await _vendorRepository.ToListAsync(cancellationToken);

        if (request is { PageSize: not null, PageIndex: not null })
        {
            output = output.ToPaged(request.PageIndex.Value, request.PageSize.Value).ToList();
        }

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        await _distributedCache.SetObjectAsync(nameof(ListVendorQuery), output, entryOptions: options, cancellationToken: cancellationToken);

        return Result<IEnumerable<VendorDTO>>.Success(MapTo(output));
    }

    private IEnumerable<VendorDTO> MapTo(IEnumerable<Vendor> vendors)
    {
        var result = new List<VendorDTO>();
        foreach (var v in vendors)
        {
            result.Add(new VendorDTO()
            {
                Name = v.Name,
                Id = v.Id,
                Phone = v.Phone,
                Mail = v.Mail,
                Address1 = v.Address.Address1,
                Address2 = v.Address.Address2,
                City = v.Address.City,
                Country = v.Address.Country,
                Name2 = v.Name2?.Value,
                Notes = v.Notes,
                Zip = v.Address.Zip

            });
        }
        return result;
    }
}