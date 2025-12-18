using LibraryManagement.Application.Libraries.Commands;

namespace LibraryManagement.Application.Tests.Libraries;

public class CreateLibraryCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_NameEmpty()
    {
        var validator = new CreateLibraryCommandValidator();
        var cmd = new CreateLibraryCommand("", "City Centre");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_AddressEmpty()
    {
        var validator = new CreateLibraryCommandValidator();
        var cmd = new CreateLibraryCommand("National Library", "");

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new CreateLibraryCommandValidator();
        var cmd = new CreateLibraryCommand("National Library", "City Centre");

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}