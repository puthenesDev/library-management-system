using LibraryManagement.Application.Members.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Members;

public class UpdateMemberCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_UpdateMember_WhenMemberExists()
    {
        // Arrange
        var member = new Member("David", "david@gmail.com");
        var newName = "David Peter";
        var newEmail = "david.peter@example.com";

        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateMemberCommandHandler(repoMock.Object);
        var command = new UpdateMemberCommand(member.Id, newName, newEmail);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal(newName, member.Name);
        Assert.Equal(newEmail, member.Email);

        repoMock.Verify(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenMemberDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        var handler = new UpdateMemberCommandHandler(repoMock.Object);
        var command = new UpdateMemberCommand(Guid.NewGuid(), "Name1", "Email1");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);

        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}