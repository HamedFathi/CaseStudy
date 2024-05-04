using HamedStack.Ensure;
using HamedStack.TheAggregateRoot;
using SinKien.IBAN4Net;

// ReSharper disable InconsistentNaming

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class BIC : SingleValueObject<string>
{
    protected BIC()
    {
    }

    public BIC(string value)
    {
        value.EnsureNotNull().EnsureNotEmpty();

        if (!BicUtils.IsValid(value, out _))
        {
            throw new ArgumentException("Invalid BIC.", nameof(value));
        }

        Value = value;

    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}