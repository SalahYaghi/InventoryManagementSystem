using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.ContactInfo.Commands.CreateContactInfo;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateContactInfoCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateContactInfoCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private static global::Contract.Features.References.ContactInfos.Commands.CreateContactInfo.CreateContactInfoCommand ValidCreateCommand(string? unique = null)
    {
        unique ??= Guid.NewGuid().ToString("N")[..8];
        return new global::Contract.Features.References.ContactInfos.Commands.CreateContactInfo.CreateContactInfoCommand
        {
            Email = $"person-{unique}@test.com",
            PhoneNumber = "+970599123456",
            AlternitavePhoneNumber = "+970598123456",
            FaxNumber = "+970222222222",
            WebsiteUrl = "https://example.com"
        };
    }

    private async Task<Domain.Contacts.ContactInfo.ContactInfo> CreateSavedContactInfoAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var contactInfo = Domain.Contacts.ContactInfo.ContactInfo.Create(
            Guid.NewGuid(),
            $"contact-{unique}@test.com",
            "+970599123456",
            "+970598123456",
            "+970222222222",
            "https://example.com").Value;
        await _context.ContactInfos.AddAsync(contactInfo, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return contactInfo;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var command = ValidCreateCommand();
        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Email, result.Value.Email);
        Assert.True(await _context.ContactInfos.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyEmail_ShouldFail()
    {
        var command = ValidCreateCommand() with { Email = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithEmptyPhoneNumber_ShouldFail()
    {
        var command = ValidCreateCommand() with { PhoneNumber = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
