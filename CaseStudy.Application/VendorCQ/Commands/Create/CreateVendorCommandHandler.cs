using CaseStudy.Domain.VendorAggregate;
using CaseStudy.Domain.VendorAggregate.DomainEvents;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;

namespace CaseStudy.Application.VendorCQ.Commands.Create;

public class CreateVendorCommandHandler : ICommandHandler<CreateVendorCommand, int>
{
    private readonly IRepository<Vendor> _vendorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVendorCommand> _vendorValidator;

    public CreateVendorCommandHandler(IRepository<Vendor> vendorRepository, IUnitOfWork unitOfWork, IValidator<CreateVendorCommand> vendorValidator)
    {
        _vendorRepository = vendorRepository;
        _unitOfWork = unitOfWork;
        _vendorValidator = vendorValidator;
    }

    public async Task<Result<int>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _vendorValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<int>(default);
        }

        var vendor = new Vendor()
        {
            Name = new Name(request.Name),
            Name2 = new Name(request.Name2),
            Address = new Address(request.Address1,request.Address2,request.City,request.Country,request.Zip),
            Mail = new Email(request.Mail),
            Notes = request.Notes,
            Phone = new Phone(request.Phone),
        };

        vendor.AddDomainEvent(new CreateVendorDomainEvent()
        {
            Mail = vendor.Mail,
            Name = vendor.Name
        });

        var output = await _vendorRepository.AddAsync(vendor, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(output.Id);
    }
}