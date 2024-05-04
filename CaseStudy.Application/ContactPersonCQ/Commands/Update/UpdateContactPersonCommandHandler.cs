using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

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
    public Task<Result> Handle(UpdateContactPersonCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}