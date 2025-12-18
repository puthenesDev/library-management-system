using LibraryManagement.Application.Members.Commands;

namespace LibraryManagement.Application.Tests.Members;
public class CreateMemberCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_NameEmpty()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("", "dummy@gmail.com");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_EmailInvalid()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("David", "not-an-email");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new CreateMemberCommandValidator();
        var cmd = new CreateMemberCommand("David", "alice@example.com");

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}