using HamedStack.CQRS;

namespace CaseStudy.Application.BankAccountCQ.Commands.Update;

public class UpdateBankAccountCommand : ICommand
{
    public int VendorId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IBAN { get; set; } = null!;
    public string BIC { get; set; } = null!;
}