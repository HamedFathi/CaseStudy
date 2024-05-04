using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Delete;

public class DeleteContactPersonCommandHandler : ICommandHandler<DeleteContactPersonCommand>
{
    private readonly IRepository<ContactPerson> _contactPersonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteContactPersonCommand> _contactPersonValidator;

    public DeleteContactPersonCommandHandler(IRepository<ContactPerson> contactPersonRepository, IUnitOfWork unitOfWork, IValidator<DeleteContactPersonCommand> contactPersonValidator)
    {
        _contactPersonRepository = contactPersonRepository;
        _unitOfWork = unitOfWork;
        _contactPersonValidator = contactPersonValidator;
    }
    public async Task<Result> Handle(DeleteContactPersonCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _contactPersonValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var bank = await _contactPersonRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bank == null)
        {
            return Result.Failure("Id not found.");
        }

        await _contactPersonRepository.DeleteAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("ContactPerson record deleted successfully.");
    }
}