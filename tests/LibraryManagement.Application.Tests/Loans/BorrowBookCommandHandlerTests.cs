using LibraryManagement.Application.Contracts.Loans;
using LibraryManagement.Application.Loans.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Loans;

public class BorrowBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateLoan_When_BookAndMemberExist()
    {
        // Arrange
        var libraryId = Guid.NewGuid();
        var book = new Book("Isbn_1", "Title1", "Author_1", 1, libraryId);
        var member = new Member("David", "david@gmail.com");

        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        var memberRepoMock = new Mock<IMemberRepository>();
        memberRepoMock
            .Setup(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var loanRepoMock = new Mock<ILoanRepository>();
        loanRepoMock
            .Setup(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        loanRepoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new BorrowBookCommandHandler(
            bookRepoMock.Object,
            memberRepoMock.Object,
            loanRepoMock.Object);

        var cmd = new BorrowBookCommand(book.Id, member.Id);

        // Act
        LoanResponse response = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(book.Id, response.BookId);
        Assert.Equal(member.Id, response.MemberId);
        Assert.True(response.IsActive);

        loanRepoMock.Verify(r => r.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Once);
        loanRepoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_BookNotFound()
    {
        // Arrange
        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        var memberRepoMock = new Mock<IMemberRepository>();
        var loanRepoMock = new Mock<ILoanRepository>();

        var handler = new BorrowBookCommandHandler(
            bookRepoMock.Object,
            memberRepoMock.Object,
            loanRepoMock.Object);

        var cmd = new BorrowBookCommand(Guid.NewGuid(), Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }
}
