using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Domain.VendorAggregate.Entities;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using Microsoft.Extensions.Caching.Distributed;
using FluentValidation;
using HamedStack.Paging;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Queries.List;

public class ListBankAccountQueryHandler : IQueryHandler<ListBankAccountQuery, IEnumerable<BankAccountDTO>>
{
    private readonly IRepository<BankAccount> _bankRepository;
    private readonly IValidator<ListBankAccountQuery> _bankValidator;
    private readonly IDistributedCache _distributedCache;

    public ListBankAccountQueryHandler(IRepository<BankAccount> bankRepository, IValidator<ListBankAccountQuery> bankValidator, IDistributedCache distributedCache)
    {
        _bankRepository = bankRepository;
        _bankValidator = bankValidator;
        _distributedCache = distributedCache;
    }

    public async Task<Result<IEnumerable<BankAccountDTO>>> Handle(ListBankAccountQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return (Result<IEnumerable<BankAccountDTO>>)validationResult.ToResult();
        }

        var cachedData = await _distributedCache.GetObjectAsync<List<BankAccount>>(nameof(ListVendorQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<IEnumerable<BankAccountDTO>>.Success(MapTo(cachedData));
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

        return Result<IEnumerable<BankAccountDTO>>.Success(MapTo(output));
    }

    private IEnumerable<BankAccountDTO> MapTo(IEnumerable<BankAccount> bankAccounts)
    {
        var result = new List<BankAccountDTO>();
        foreach (var b in bankAccounts)
        {
            result.Add(new BankAccountDTO()
            {
                Name = b.Name,
                Id = b.Id,
                BIC = b.BIC,
                IBAN = b.IBAN
            });
        }
        return result;
    }
}