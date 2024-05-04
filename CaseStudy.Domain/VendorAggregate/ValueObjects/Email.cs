using HamedStack.Ensure;
using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class Email : SingleValueObject<string>
{
    protected Email()
    {
    }

    public Email(string value)
    {
        Value = value.EnsureNotNull().EnsureNotEmpty().EnsureEmail();
    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}