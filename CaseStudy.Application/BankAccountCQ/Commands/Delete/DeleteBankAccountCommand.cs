using HamedStack.CQRS;

namespace CaseStudy.Application.BankAccountCQ.Commands.Delete;

public class DeleteBankAccountCommand : ICommand
{
    public int Id { get; set; }
    public int VendorId { get; set; }

}