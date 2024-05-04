using CaseStudy.Domain.VendorAggregate.Entities;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Update;

public class UpdateContactPersonCommandHandler : ICommandHandler<UpdateContactPersonCommand>
{
    private readonly IRepository<ContactPerson> _contactPersonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateContactPersonCommand> _contactPersonValidator;

    public UpdateContactPersonCommandHandler(IRepository<ContactPerson> contactPersonRepository, IUnitOfWork unitOfWork, IValidator<UpdateContactPersonCommand> contactPersonValidator)
    {
        _contactPersonRepository = contactPersonRepository;
        _unitOfWork = unitOfWork;
        _contactPersonValidator = contactPersonValidator;
    }
    public async Task<Result> Handle(UpdateContactPersonCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _contactPersonValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var bank = new ContactPerson()
        {
            Email = new Email(request.Email),
            FirstName = new Name(request.FirstName),
            LastName = new Name(request.LastName),
            Phone = new Phone(request.Phone),
            Id = request.Id,
        };

        await _contactPersonRepository.UpdateAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("ContactPerson record updated successfully.");
    }
}