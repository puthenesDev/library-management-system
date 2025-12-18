using LibraryManagement.Application.Books.Commands;

namespace LibraryManagement.Application.Tests.Books;

public class CreateBookCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_TitleEmpty()
    {
        var validator = new CreateBookCommandValidator();
        var cmd = new CreateBookCommand(Guid.NewGuid(), "1234567890", "", "Author", 1);

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new CreateBookCommandValidator();
        var cmd = new CreateBookCommand(Guid.NewGuid(), "1234567890", "Title", "Author", 1);

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}