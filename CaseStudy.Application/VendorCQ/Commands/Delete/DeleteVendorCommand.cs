using HamedStack.CQRS;

namespace CaseStudy.Application.VendorCQ.Commands.Delete;

public class DeleteVendorCommand : ICommand
{
    public int Id { get; set; }
}