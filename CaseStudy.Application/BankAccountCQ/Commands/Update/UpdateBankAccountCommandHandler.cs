using CaseStudy.Domain.VendorAggregate.Entities;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Update;

public class UpdateBankAccountCommandHandler : ICommandHandler<UpdateBankAccountCommand>
{
    private readonly IRepository<BankAccount> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateBankAccountCommand> _bankValidator;

    public UpdateBankAccountCommandHandler(IRepository<BankAccount> bankRepository, IUnitOfWork unitOfWork, IValidator<UpdateBankAccountCommand> bankValidator)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
        _bankValidator = bankValidator;
    }

    public async Task<Result> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var bank = new BankAccount()
        {
            Id = request.Id,
            Name = new Name(request.Name),
            BIC = new BIC(request.BIC),
            IBAN = new IBAN(request.IBAN),
        };

        await _bankRepository.UpdateAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Bank record updated successfully.");
    }
}