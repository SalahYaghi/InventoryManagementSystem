using Contract.Features.User.Commands.CreateUser;
using Domain.Identity.Users;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            "P@ssw0rd123!",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyEmail_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            "P@ssw0rd123!",
            Role.Admin,
            string.Empty,
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.email);
    }

    [Fact]
    public async Task Validate_WithInvalidEmail_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            "P@ssw0rd123!",
            Role.Admin,
            "invalid-email",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.email);
    }

    [Fact]
    public async Task Validate_WithEmptyUsername_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            string.Empty,
            "P@ssw0rd123!",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithUsernameStartingWithNumber_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "1user_name",
            "P@ssw0rd123!",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithShortUsername_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "abc",
            "P@ssw0rd123!",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithLongUsername_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "username_is_too_long_for_rule",
            "P@ssw0rd123!",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.username);
    }

    [Fact]
    public async Task Validate_WithEmptyPassword_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            string.Empty,
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.password);
    }

    [Fact]
    public async Task Validate_WithWeakPassword_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            "password",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.password);
    }

    [Fact]
    public async Task Validate_WithPasswordMissingDigit_ShouldHaveValidationError()
    {
        var command = new CreateUserCommand(
            "valid_user",
            "assword",
            Role.Admin,
            "valid.user@test.com",
            Guid.NewGuid());

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.password);
    }
}
