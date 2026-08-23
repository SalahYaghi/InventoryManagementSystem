using Contract.Features.User.Commands.CreateUser;
using Domain.Identity.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidatorTests
{
    private readonly UpdateUserCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "valid_user",
            "valid.user@test.com",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyEmail_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "valid_user",
            string.Empty,
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.email);
    }

    [Fact]
    public async Task Validate_WithInvalidEmail_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "valid_user",
            "invalid-email",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.email);
    }

    [Fact]
    public async Task Validate_WithEmptyUsername_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            string.Empty,
            "valid.user@test.com",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithUsernameStartingWithNumber_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "1user_name",
            "valid.user@test.com",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithShortUsername_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "abc",
            "valid.user@test.com",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithLongUsername_ShouldHaveValidationError()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            "username_is_too_long_for_rule",
            "valid.user@test.com",
            true,
            Role.Admin);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }
}
