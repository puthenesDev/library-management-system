using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Application.Members.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Members;

public class CreateMemberCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_AddMember()
    {
        // Arrange
        var repoMock = new Mock<IMemberRepository>();

        repoMock
            .Setup(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateMemberCommandHandler(repoMock.Object);

        var cmd = new CreateMemberCommand("Alice", "alice@example.com");

        // Act
        MemberResponse response = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Alice", response.Name);

        repoMock.Verify(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
