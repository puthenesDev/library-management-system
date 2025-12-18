using LibraryManagement.Application.Books.Commands;

namespace LibraryManagement.Application.Tests.Books;

public class CreateBookCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_TitleEmpty()
    {
        var validator = new CreateBookCommandValidator();
        var cmd = new CreateBookCommand(Guid.NewGuid(), "Isbn1", "", "Peter", 1);

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new CreateBookCommandValidator();
        var cmd = new CreateBookCommand(Guid.NewGuid(), "Isbn123456789", "Title", "Peter", 1);

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}