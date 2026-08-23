using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.ContactInfo.Queries.GetContactInfoPaged;

public class GetContactInfoPagedQueryValidatorTests
{
    private readonly global::Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged.GetContactInfoPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidPaging_ShouldNotHaveValidationError()
    {
        var query = new global::Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged.GetContactInfoPagedQuery { PageNumber = 1, PageSize = 10 };
        var result = await _validator.TestValidateAsync(query);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithZeroPageNumber_ShouldHaveValidationError()
    {
        var query = new global::Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged.GetContactInfoPagedQuery { PageNumber = 0, PageSize = 10 };
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public async Task Validate_WithZeroPageSize_ShouldHaveValidationError()
    {
        var query = new global::Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged.GetContactInfoPagedQuery { PageNumber = 1, PageSize = 0 };
        var result = await _validator.TestValidateAsync(query);
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}
