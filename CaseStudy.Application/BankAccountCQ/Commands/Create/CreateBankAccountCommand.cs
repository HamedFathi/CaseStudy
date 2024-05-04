using HamedStack.CQRS;
// ReSharper disable InconsistentNaming

namespace CaseStudy.Application.BankAccountCQ.Commands.Create;

public class CreateBankAccountCommand : ICommand<int>
{
    public int VendorId { get; set; }
    public string Name { get; set; } = null!;
    public string IBAN { get; set; } = null!;
    public string BIC { get; set; } = null!;
}