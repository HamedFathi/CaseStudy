using CaseStudy.Domain.VendorAggregate.Entities;
using CaseStudy.Domain.VendorAggregate.ValueObjects;
using HamedStack.TheAggregateRoot;
using HamedStack.TheAggregateRoot.Abstractions;

namespace CaseStudy.Domain.VendorAggregate;

public class Vendor : Entity<int>, IAggregateRoot
{
    public Name Name { get; set; } = null!;
    public Name? Name2 { get; set; }
    public Address Address { get; set; } = null!;
    public Email Mail { get; set; } = null!;
    public Phone Phone { get; set; } = null!;
    public string? Notes { get; set; }

    private readonly List<BankAccount> _bankAccounts = new();
    private readonly List<ContactPerson> _contactPeople = new();

    public IReadOnlyCollection<BankAccount> BankAccounts => _bankAccounts.AsReadOnly();
    public IReadOnlyCollection<ContactPerson> ContactPeople => _contactPeople.AsReadOnly();

    public void AddBankAccount(BankAccount bankAccount)
    {
        if (bankAccount == null) throw new ArgumentNullException(nameof(bankAccount));
        _bankAccounts.Add(bankAccount);
    }

    public void AddContactPerson(ContactPerson contactPerson)
    {
        if (contactPerson == null) throw new ArgumentNullException(nameof(contactPerson));
        _contactPeople.Add(contactPerson);
    }

    public void RemoveBankAccount(int bankAccountId)
    {
        var bankAccount = _bankAccounts.FirstOrDefault(ba => ba.Id == bankAccountId);
        if (bankAccount != null)
        {
            _bankAccounts.Remove(bankAccount);
        }
    }

    public void RemoveContactPerson(int contactPersonId)
    {
        var contact = _contactPeople.FirstOrDefault(cp => cp.Id == contactPersonId);
        if (contact != null)
        {
            _contactPeople.Remove(contact);
        }
    }

    public void UpdateBankAccount(BankAccount bankAccount)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        var bankAccountData = _bankAccounts.FirstOrDefault(ba => ba.Id == bankAccount.Id);
        if (bankAccountData != null)
        {
            bankAccountData.BIC = bankAccount.BIC;
            bankAccountData.IBAN = bankAccount.IBAN;
            bankAccountData.Name = bankAccount.Name;
        }
    }

    public void UpdateContactPerson(ContactPerson contactPerson)
    {
        ArgumentNullException.ThrowIfNull(contactPerson);
        var contactPersonData = _contactPeople.FirstOrDefault(cp => cp.Id == contactPerson.Id);
        if (contactPersonData == null) return;
        contactPersonData.FirstName = contactPerson.FirstName;
        contactPersonData.LastName = contactPerson.LastName;
        contactPersonData.Email = contactPerson.Email;
        contactPersonData.Phone = contactPerson.Phone;
    }
}