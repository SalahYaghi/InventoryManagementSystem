using Xunit;

namespace SubcutaneousTests.Features.Parties.Customer.Queries.GetCustomerPaged;

public class GetCustomerPagedQueryValidatorTests
{
    [Fact]
    public void Query_HasNoRules_ShouldBeCreatable()
    {
        var query = new global::Contract.Features.Parties.Customers.Queries.GetCustomerPaged.GetCustomerQuery();
        Assert.NotNull(query.CacheKey);
        Assert.NotEmpty(query.Tags);
    }
}
