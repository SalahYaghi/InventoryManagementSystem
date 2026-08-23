using Xunit;

namespace SubcutaneousTests.Features.Parties.Supplier.Queries.GetSupplierPaged;

public class GetSupplierPagedQueryValidatorTests
{
    [Fact]
    public void Query_HasNoRules_ShouldBeCreatable()
    {
        var query = new global::Contract.Features.Parties.Supplier.Queries.GetSupplierPaged.GetSupplierPagedQuery();
        Assert.NotNull(query.CacheKey);
        Assert.NotEmpty(query.Tags);
    }
}
