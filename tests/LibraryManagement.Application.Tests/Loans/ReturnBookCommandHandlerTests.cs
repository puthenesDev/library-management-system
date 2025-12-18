using LibraryManagement.Application.Contracts.Loans;
using LibraryManagement.Application.Loans.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Loans;

public class ReturnBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnLoanAndBook()
    {
        // Arrange
        var libraryId = Guid.NewGuid();
        var book = new Book("Isbn_1", "Title_1", "Author_1", 1, libraryId);
        book.Borrow(); 

        var loan = new Loan(book.Id, Guid.NewGuid(), DateTime.UtcNow, TimeSpan.FromDays(14));

        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);
        bookRepoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var loanRepoMock = new Mock<ILoanRepository>();
        loanRepoMock
            .Setup(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);
        loanRepoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new ReturnBookCommandHandler(loanRepoMock.Object, bookRepoMock.Object);
        var cmd = new ReturnBookCommand(loan.Id);

        // Act
        LoanResponse response = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsActive);
        Assert.NotNull(response.ReturnedAt);
        Assert.Equal(book.TotalCopies, book.AvailableCopies);

        loanRepoMock.Verify(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()), Times.Once);
        loanRepoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        bookRepoMock.Verify(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()), Times.Once);
        bookRepoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_LoanNotFound()
    {
        // Arrange
        var loanRepoMock = new Mock<ILoanRepository>();
        loanRepoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Loan?)null);

        var bookRepoMock = new Mock<IBookRepository>();

        var handler = new ReturnBookCommandHandler(loanRepoMock.Object, bookRepoMock.Object);
        var cmd = new ReturnBookCommand(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }
}
