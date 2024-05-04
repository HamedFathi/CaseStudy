using HamedStack.Ensure;
using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class Phone : SingleValueObject<string>
{
    protected Phone()
    {
    }

    public Phone(string value)
    {
        Value = value.EnsureNotNull().EnsureNotEmpty().EnsureRegex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}