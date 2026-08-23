using Contract.Features.Parties.People.Queries.GetPersonPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.Person.Queries.GetPersonPaged;

public class GetPersonPagedQueryValidatorTests
{
    private readonly GetPersonPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidPaging_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetPersonPagedQuery { PageNumber = 1, PageSize = 10 });
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithInvalidPageNumber_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetPersonPagedQuery { PageNumber = 0, PageSize = 10 });
        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public async Task Validate_WithInvalidPageSize_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetPersonPagedQuery { PageNumber = 1, PageSize = 0 });
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}
