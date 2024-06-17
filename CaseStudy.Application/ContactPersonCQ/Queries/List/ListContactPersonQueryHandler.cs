using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.Paging;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CaseStudy.Application.ContactPersonCQ.Queries.List;

public class ListContactPersonQueryHandler : IQueryHandler<ListContactPersonQuery, IEnumerable<ContactPersonDTO>>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.ContactPerson> _bankRepository;
    private readonly IValidator<ListContactPersonQuery> _contactPersonValidator;
    private readonly IDistributedCache _distributedCache;

    public ListContactPersonQueryHandler(IRepository<Domain.VendorAggregate.Entities.ContactPerson> bankRepository, IValidator<ListContactPersonQuery> contactPersonValidator, IDistributedCache distributedCache)
    {
        _bankRepository = bankRepository;
        _contactPersonValidator = contactPersonValidator;
        _distributedCache = distributedCache;
    }
    public async Task<Result<IEnumerable<ContactPersonDTO>>> Handle(ListContactPersonQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _contactPersonValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<IEnumerable<ContactPersonDTO>>(default);
        }

        var cachedData = await _distributedCache.GetObjectAsync<List<ContactPerson>>(nameof(ListVendorQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<IEnumerable<ContactPersonDTO>>.Success(MapTo(cachedData));
        }

        var output = await _bankRepository.ToListAsync(cancellationToken);

        if (request is { PageSize: not null, PageIndex: not null })
        {
            output = output.ToPaged(request.PageIndex.Value, request.PageSize.Value).ToList();
        }

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        await _distributedCache.SetObjectAsync(nameof(ListVendorQuery), output, entryOptions: options, cancellationToken: cancellationToken);

        return Result<IEnumerable<ContactPersonDTO>>.Success(MapTo(output));
    }

    private IEnumerable<ContactPersonDTO> MapTo(IEnumerable<ContactPerson> contactPeople)
    {
        var result = new List<ContactPersonDTO>();
        foreach (var cp in contactPeople)
        {
            result.Add(new ContactPersonDTO()
            {
                Id = cp.Id,
                Phone = cp.Phone,
                Email = cp.Email,
                FirstName = cp.FirstName,
                LastName = cp.LastName,
            });
        }
        return result;
    }
}