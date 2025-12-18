using LibraryManagement.Application.Loans.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Loans;
public class DeleteLoanCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue_WhenLoanExists()
    {
        // Arrange
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, TimeSpan.FromDays(14));

        var repoMock = new Mock<ILoanRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);
        repoMock
            .Setup(r => r.DeleteAsync(loan, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteLoanCommandHandler(repoMock.Object);
        var command = new DeleteLoanCommand(loan.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        repoMock.Verify(r => r.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(loan, It.IsAny<CancellationToken>()), Times.Once);
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

        var handler = new DeleteLoanCommandHandler(repoMock.Object);
        var command = new DeleteLoanCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Never);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
