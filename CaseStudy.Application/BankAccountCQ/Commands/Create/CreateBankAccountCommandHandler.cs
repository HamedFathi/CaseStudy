using CaseStudy.Domain.VendorAggregate.Entities;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Create;

public class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, int>
{
    private readonly IRepository<BankAccount> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateBankAccountCommand> _bankValidator;

    public CreateBankAccountCommandHandler(IRepository<BankAccount> bankRepository, IUnitOfWork unitOfWork, IValidator<CreateBankAccountCommand> bankValidator)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
        _bankValidator = bankValidator;
    }

    public async Task<Result<int>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return (Result<int>)validationResult.ToResult();
        }

        var bank = new BankAccount()
        {
            Name = (Name)request.Name,
            BIC = (BIC)request.BIC,
            IBAN = (IBAN)request.IBAN,
        };

        var output = await _bankRepository.AddAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(output.Id);

    }
}