using CaseStudy.Domain.VendorAggregate.Entities;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;

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
    public Task<Result> Handle(DeleteContactPersonCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}