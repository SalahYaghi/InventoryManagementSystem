using MechanicShop.Domain.Common.Results;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Common;

public class ResultTests
{
    [Fact]
    public void ImplicitConversion_FromValue_CreatesSuccessResult()
    {
        Result<string> result = "hello";

        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
        Assert.Equal("hello", result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ImplicitConversion_FromError_CreatesErrorResult()
    {
        var error = Error.Validation("Test.Code", "Test description");
        Result<string> result = error;

        Assert.False(result.IsSuccess);
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal("Test.Code", result.TopError.Code);
        Assert.Equal(ErrorKind.Validation, result.TopError.Type);
    }

    [Fact]
    public void ImplicitConversion_FromErrorList_CreatesErrorResult()
    {
        var errors = new List<Error>
        {
            Error.Validation("A", "a"),
            Error.Conflict("B", "b")
        };

        Result<int> result = errors;

        Assert.True(result.IsError);
        Assert.Equal(2, result.Errors.Count);
        Assert.Equal("A", result.TopError.Code);
    }

    [Fact]
    public void ImplicitConversion_FromEmptyErrorList_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Result<int> result = new List<Error>();
        });
    }

    [Fact]
    public void ImplicitConversion_FromNullValue_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Result<string> result = (string)null!;
        });
    }

    [Fact]
    public void Match_OnSuccess_InvokesOnValue()
    {
        Result<int> result = 42;

        var output = result.Match(v => v * 2, errs => -1);

        Assert.Equal(84, output);
    }

    [Fact]
    public void Match_OnError_InvokesOnError()
    {
        Result<int> result = Error.NotFound("X", "missing");

        var output = result.Match(v => v * 2, errs => errs.Count);

        Assert.Equal(1, output);
    }

    [Fact]
    public void Value_OnErrorResult_ReturnsDefault_DoesNotThrow()
    {
        // Documents current behavior: accessing Value on an error result
        // silently returns default instead of throwing. Callers MUST check
        // IsSuccess first — consider throwing instead to fail fast.
        Result<string> result = Error.Failure("X", "boom");

        Assert.Null(result.Value);
    }

    [Fact]
    public void TopError_OnSuccess_ReturnsDefaultError()
    {
        Result<int> result = 5;

        Assert.Equal(default, result.TopError);
    }

    [Fact]
    public void ErrorFactories_AssignCorrectKind()
    {
        Assert.Equal(ErrorKind.Failure, Error.Failure().Type);
        Assert.Equal(ErrorKind.Unexpected, Error.Unexpected().Type);
        Assert.Equal(ErrorKind.Validation, Error.Validation().Type);
        Assert.Equal(ErrorKind.Conflict, Error.Conflict().Type);
        Assert.Equal(ErrorKind.NotFound, Error.NotFound().Type);
        Assert.Equal(ErrorKind.Unauthorized, Error.Unauthorized().Type);
        Assert.Equal(ErrorKind.Forbidden, Error.Forbidden().Type);
    }
}
