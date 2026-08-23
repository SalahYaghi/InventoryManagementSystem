using Xunit;
namespace SubcutaneousTests.Features.References.Country.Queries.GetCountryPaged;

public class GetCountryPagedQueryValidatorTests
{
    [Fact]
    public void Validator_HasNoRules_ShouldAllowDefaultQuery()
    {
        var validator = new global::Contract.Features.References.Countries.Queries.GetCountryPaged.GetCountryPagedQueryValidator();
        var result = validator.Validate(new global::Contract.Features.References.Countries.Queries.GetCountryPaged.GetCountryPagedQuery());
        Assert.True(result.IsValid);
    }
}
