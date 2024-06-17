using CaseStudy.Application.VendorCQ.Queries.List;
using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.Cache.Extensions.DistributedCacheExtended;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

namespace CaseStudy.Application.BankAccountCQ.Queries.Get;

public class GetBankAccountByIdQueryHandler : IQueryHandler<GetBankAccountByIdQuery, BankAccountDTO>
{
    private readonly IRepository<BankAccount> _bankRepository;
    private readonly IValidator<GetBankAccountByIdQuery> _bankValidator;
    private readonly IDistributedCache _distributedCache;

    public GetBankAccountByIdQueryHandler(IRepository<BankAccount> bankRepository, IValidator<GetBankAccountByIdQuery> bankValidator, IDistributedCache distributedCache)
    {
        _bankRepository = bankRepository;
        _bankValidator = bankValidator;
        _distributedCache = distributedCache;
    }

    public async Task<Result<BankAccountDTO>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<BankAccountDTO>(default);
        }

        var cachedData = await _distributedCache.GetObjectAsync<BankAccount>(nameof(ListVendorQuery), cancellationToken: cancellationToken);
        if (cachedData != null)
        {
            return Result<BankAccountDTO>.Success(MapTo(cachedData));
        }

        var output = await _bankRepository.GetByIdsAsync(new object[] { request.Id, request.VendorId }, cancellationToken);

        if (output == null)
        {
            return (Result<BankAccountDTO>)Result.Failure("Id not found.");
        }

        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        await _distributedCache.SetObjectAsync(nameof(ListVendorQuery), output, entryOptions: options, cancellationToken: cancellationToken);

        return Result<BankAccountDTO>.Success(MapTo(output));
    }

    private BankAccountDTO MapTo(BankAccount bankAccount)
    {
        return new BankAccountDTO()
        {
            Name = bankAccount.Name,
            BIC = bankAccount.BIC,
            IBAN = bankAccount.IBAN,
            Id = bankAccount.Id
        };
    }
}