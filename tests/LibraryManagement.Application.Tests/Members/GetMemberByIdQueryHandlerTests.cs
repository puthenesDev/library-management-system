using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Application.Members.Queries;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Members;

public class GetMemberByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnMember_WhenExists()
    {
        // Arrange
        var member = new Member("David", "david@gmail.com");

        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        var handler = new GetMemberByIdQueryHandler(repoMock.Object);

        // Act
        MemberResponse? result = await handler.Handle(new GetMemberByIdQuery(member.Id), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(member.Id, result!.Id);

        repoMock.Verify(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnNull_WhenNotFound()
    {
        // Arrange
        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        var handler = new GetMemberByIdQueryHandler(repoMock.Object);

        // Act
        var result = await handler.Handle(new GetMemberByIdQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.Null(result);

        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
