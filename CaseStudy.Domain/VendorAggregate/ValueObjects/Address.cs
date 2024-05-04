using HamedStack.TheAggregateRoot;

namespace CaseStudy.Domain.VendorAggregate.ValueObjects;

public class Address : ValueObject
{
    protected Address()
    {
    }

    public Address(string address1, string address2, string city, string country, string zip)
    {
        Address1 = address1;
        Address2 = address2;
        City = city;
        Country = country;
        Zip = zip;
    }

    public string Address1 { get; } = null!;
    public string Address2 { get; } = null!;
    public string City { get; } = null!;
    public string Country { get; } = null!;
    public string Zip { get; } = null!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address1.Trim().ToLower();
        yield return Address2.Trim().ToLower();
        yield return City.Trim().ToLower();
        yield return Country.Trim().ToLower();
        yield return Zip.Trim().ToLower();
    }
}