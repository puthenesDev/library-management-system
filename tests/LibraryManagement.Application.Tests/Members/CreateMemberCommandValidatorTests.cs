using LibraryManagement.Application.Members.Commands;

namespace LibraryManagement.Application.Tests.Members;
public class CreateMemberCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_NameEmpty()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("", "test@example.com");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_EmailInvalid()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("Alice", "not-an-email");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("Alice", "alice@example.com");

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}