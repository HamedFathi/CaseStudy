using HamedStack.Ensure;
using HamedStack.TheAggregateRoot;
using SinKien.IBAN4Net;
// ReSharper disable InconsistentNaming

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class IBAN : SingleValueObject<string>
{
    protected IBAN()
    {
    }

    public IBAN(string value)
    {
        value.EnsureNotNull().EnsureNotEmpty();

        if (!IbanUtils.IsValid(value, out _))
        {
            throw new ArgumentException("Invalid IBAN.", nameof(value));
        }

        Value = value;

    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}