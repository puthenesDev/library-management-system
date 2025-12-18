using LibraryManagement.Application.Loans.Commands;

namespace LibraryManagement.Application.Tests.Loans;

public class BorrowBookCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_BookIdEmpty()
    {
        var validator = new BorrowBookCommandValidator();
        var cmd = new BorrowBookCommand(Guid.Empty, Guid.NewGuid());

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_MemberIdEmpty()
    {
        var validator = new BorrowBookCommandValidator();
        var cmd = new BorrowBookCommand(Guid.NewGuid(), Guid.Empty);

        var result = validator.Validate(cmd);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var validator = new BorrowBookCommandValidator();
        var cmd = new BorrowBookCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = validator.Validate(cmd);
        Assert.True(result.IsValid);
    }
}