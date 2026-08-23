using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.ContactInfo.Commands.UpdateContactInfo;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateContactInfoCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateContactInfoCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
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
        var contactInfo = await CreateSavedContactInfoAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];
        var command = new global::Contract.Features.References.ContactInfos.Commands.UpdateContactInfo.UpdateContactInfoCommand
        {
            Id = contactInfo.Id,
            Email = $"updated-{unique}@test.com",
            PhoneNumber = "+970599999999",
            AlternitavePhoneNumber = "+970598888888",
            FaxNumber = "+970222222222",
            WebsiteUrl = "https://updated.example.com"
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Email, result.Value.Email);
    }

    [Fact]
    public async Task Handle_WithMissingContactInfo_ShouldFail()
    {
        var command = new global::Contract.Features.References.ContactInfos.Commands.UpdateContactInfo.UpdateContactInfoCommand { Id = Guid.NewGuid(), Email = "missing@test.com", PhoneNumber = "+970599999999", AlternitavePhoneNumber = "+970598888888", FaxNumber = "+970222222222", WebsiteUrl = "https://example.com" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "ContactInfo.NotFound");
    }

    [Fact]
    public async Task Handle_WithEmptyEmail_ShouldFail()
    {
        var contactInfo = await CreateSavedContactInfoAsync();
        var command = new global::Contract.Features.References.ContactInfos.Commands.UpdateContactInfo.UpdateContactInfoCommand { Id = contactInfo.Id, Email = string.Empty, PhoneNumber = "+970599999999", AlternitavePhoneNumber = "+970598888888", FaxNumber = "+970222222222", WebsiteUrl = "https://example.com" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
