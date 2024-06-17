using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CaseStudy.Application.ContactPersonCQ.Queries.Get;

public class GetContactPersonByIdQueryHandler : IQueryHandler<GetContactPersonByIdQuery, ContactPersonDTO>
{
    private readonly IRepository<ContactPerson> _bankRepository;
    private readonly IValidator<GetContactPersonByIdQuery> _contactPersonValidator;
    private readonly IDistributedCache _distributedCache;

    public GetContactPersonByIdQueryHandler(IRepository<ContactPerson> bankRepository, IValidator<GetContactPersonByIdQuery> contactPersonValidator, IDistributedCache distributedCache)
    {
        _bankRepository = bankRepository;
        _contactPersonValidator = contactPersonValidator;
        _distributedCache = distributedCache;
    }
    public async Task<Result<ContactPersonDTO>> Handle(GetContactPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _contactPersonValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ContactPersonDTO>(default);
        }

        var cachedData = await _distributedCache.GetObjectAsync<ContactPerson>(nameof(ListVendorQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<ContactPersonDTO>.Success(MapTo(cachedData));
        }

        var output = await _bankRepository.GetByIdsAsync(new object[] { request.Id, request.VendorId }, cancellationToken);

        if (output == null)
        {
            return (Result<ContactPersonDTO>)Result.Failure("Id not found.");
        }

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        await _distributedCache.SetObjectAsync(nameof(ListVendorQuery), output, entryOptions: options, cancellationToken: cancellationToken);

        return Result<ContactPersonDTO>.Success(MapTo(output));
    }

    private ContactPersonDTO MapTo(ContactPerson contactPerson)
    {
        return new ContactPersonDTO()
        {
            Id = contactPerson.Id,
            Phone = contactPerson.Phone,
            Email = contactPerson.Email,
            FirstName = contactPerson.FirstName,
            LastName = contactPerson.LastName,
        };
    }
}