using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

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
    public Task<Result<int>> Handle(CreateContactPersonCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}