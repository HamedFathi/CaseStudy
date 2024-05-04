using CaseStudy.Domain.VendorAggregate;
using CaseStudy.Domain.VendorAggregate.DomainEvents;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Update;

public class UpdateVendorCommandHandler : ICommandHandler<UpdateVendorCommand>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVendorCommand> _vendorValidator;

    public UpdateVendorCommandHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork, IValidator<UpdateVendorCommand> vendorValidator)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
        _vendorValidator = vendorValidator;
    }

    public async Task<Result> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _vendorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        var vendor = new Vendor()
        {
            Name = (Name)request.Name,
            Name2 = (Name)request.Name2,
            Address = new Address(request.Address1, request.Address2, request.City, request.Country, request.Zip),
            Mail = (Email)request.Mail,
            Notes = request.Notes,
            Phone = (Phone)request.Phone,
        };

        vendor.AddDomainEvent(new UpdateVendorDomainEvent()
        {
            Id = vendor.Id,
            Mail = vendor.Mail,
            Name = vendor.Name
        });

        await _vendorRepository.UpdateAsync(vendor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Vendor record updated successfully.");
    }
}