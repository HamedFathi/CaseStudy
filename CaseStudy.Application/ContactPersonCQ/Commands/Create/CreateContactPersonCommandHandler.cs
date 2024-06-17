using CaseStudy.Domain.VendorAggregate.Entities;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.ContactPersonCQ.Commands.Create;

public class CreateContactPersonCommandHandler : ICommandHandler<CreateContactPersonCommand, int>
{
    private readonly IRepository<ContactPerson> _contactPersonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateContactPersonCommand> _contactPersonValidator;

    public CreateContactPersonCommandHandler(IRepository<ContactPerson> contactPersonRepository, IUnitOfWork unitOfWork, IValidator<CreateContactPersonCommand> contactPersonValidator)
    {
        _contactPersonRepository = contactPersonRepository;
        _unitOfWork = unitOfWork;
        _contactPersonValidator = contactPersonValidator;
    }
    public async Task<Result<int>> Handle(CreateContactPersonCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _contactPersonValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<int>(default);
        }

        var bank = new ContactPerson()
        {
            Email = new Email(request.Email),
            FirstName = new Name(request.FirstName),
            LastName = new Name(request.LastName),
            Phone = new Phone(request.Phone)
        };

        var output = await _contactPersonRepository.AddAsync(bank, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(output.Id);
    }
}