using LibraryManagement.Application.Loans.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Loans;

public class UpdateLoanCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_UpdateLoan_WhenLoanExists()
    {
        // Arrange
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, TimeSpan.FromDays(14));
        var newDueDate = DateTime.UtcNow.AddDays(30);

        var repoMock = new Mock<ILoanRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateLoanCommandHandler(repoMock.Object);
        var command = new UpdateLoanCommand(loan.Id, newDueDate);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal(newDueDate, loan.DueDate);

        repoMock.Verify(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenLoanDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<ILoanRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Loan?)null);

        var handler = new UpdateLoanCommandHandler(repoMock.Object);
        var command = new UpdateLoanCommand(Guid.NewGuid(), DateTime.UtcNow.AddDays(10));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);

        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}