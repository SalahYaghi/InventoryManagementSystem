using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.City.Queries.GetCityPaged;

public class GetCityPagedQueryValidatorTests
{
    private readonly global::Contract.Features.References.Cities.Queries.GetCityPaged.GetCityPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidCountryId_ShouldNotHaveValidationError()
    {
        var query = new global::Contract.Features.References.Cities.Queries.GetCityPaged.GetCityByCountryIdPagedQuery(Guid.NewGuid());
        var result = await _validator.TestValidateAsync(query);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyCountryId_ShouldHaveValidationError()
    {
        var query = new global::Contract.Features.References.Cities.Queries.GetCityPaged.GetCityByCountryIdPagedQuery(Guid.Empty);
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.CountryId);
    }
}
