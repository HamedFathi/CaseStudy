using CaseStudy.Domain.VendorAggregate;
using CaseStudy.Domain.VendorAggregate.DomainEvents;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Delete;

public class DeleteVendorCommandHandler : ICommandHandler<DeleteVendorCommand>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteVendorCommand> _vendorValidator;

    public DeleteVendorCommandHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork, IValidator<DeleteVendorCommand> vendorValidator)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
        _vendorValidator = vendorValidator;
    }

    public async Task<Result> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _vendorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var vendor = await _vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (vendor == null)
        {
            return Result.Failure("Id not found.");
        }

        vendor.AddDomainEvent(new DeleteVendorDomainEvent()
        {
            Id = vendor.Id,
        });

        await _vendorRepository.DeleteAsync(vendor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Vendor record deleted successfully.");

    }
}