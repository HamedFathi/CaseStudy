using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.BankAccountCQ.Commands.Delete;

public class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand>
{
    private readonly IRepository<Domain.VendorAggregate.Entities.BankAccount> _bankRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBankAccountCommand> _bankValidator;


    public DeleteBankAccountCommandHandler(IRepository<Domain.VendorAggregate.Entities.BankAccount> bankRepository, IUnitOfWork unitOfWork, IValidator<DeleteBankAccountCommand> bankValidator)
    {
        _bankRepository = bankRepository;
        _unitOfWork = unitOfWork;
        _bankValidator = bankValidator;
    }

    public async Task<Result> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var bank = await _bankRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bank == null)
        {
            return Result.Failure("Id not found.");
        }

        await _bankRepository.DeleteAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Bank record deleted successfully.");
    }
}