using Contract.Features.User.Commands.CreateUser;
using FluentValidation.TestHelper;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SubcutaneousTests.Features.Users.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandValidatorTests
{
    private readonly UpdateUserPasswordCommandValidator _validator = new();

    private readonly ITestOutputHelper _output;

    public UpdateUserPasswordCommandValidatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "P@ssw0rd123!",
            "N3wP@ssword!");

        var result = await _validator.TestValidateAsync(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyOldPassword_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            string.Empty,
            "N3wP@ssword!");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.oldpassword);
    }

    [Fact]
    public async Task Validate_WithWeakOldPassword_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "password",
            "N3wP@ssword!");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.oldpassword);
    }

    [Fact]
    public async Task Validate_WithOldPasswordMissingDigit_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "Password",
            "N3wP@ssword!");

        var result = await _validator.TestValidateAsync(command);

        _output.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));

        result.ShouldHaveValidationErrorFor(x => x.oldpassword);
    }

    [Fact]
    public async Task Validate_WithEmptyNewPassword_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "P@ssw0rd123!",
            string.Empty);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.newpassword);
    }

    [Fact]
    public async Task Validate_WithWeakNewPassword_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "P@ssw0rd123!",
            "password");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.newpassword);
    }

    [Fact]
    public async Task Validate_WithNewPasswordMissingUppercase_ShouldHaveValidationError()
    {
        var command = new UpdateUserPasswordCommand(
            Guid.NewGuid(),
            "P@ssw0rd123!",
            "newpssword");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.newpassword);
    }
}
