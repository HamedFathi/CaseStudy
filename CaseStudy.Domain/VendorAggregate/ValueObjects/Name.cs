using HamedStack.Ensure;
using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class Name : SingleValueObject<string>
{
    protected Name()
    {

    }
    public Name(string value)
    {
        Value = value.EnsureNotNull().EnsureNotEmpty();
    }
    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}