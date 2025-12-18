using LibraryManagement.Application.Members.Queries;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Members;

public class GetMembersQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnMappedMemberResponses_WhenMembersExist()
    {
        // Arrange
        var members = new List<Member>
            {
                new Member("David", "david@gmail.com"),
                new Member("Matt", "matt@gmail.com")
            };

        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(members);

        var handler = new GetMembersQueryHandler(repoMock.Object);
        var query = new GetMembersQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(members[0].Id, result[0].Id);
        Assert.Equal(members[0].Name, result[0].Name);
        Assert.Equal(members[0].Email, result[0].Email);
        Assert.Equal(members[1].Id, result[1].Id);
        Assert.Equal(members[1].Name, result[1].Name);
        Assert.Equal(members[1].Email, result[1].Email);

        repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoMembersExist()
    {
        // Arrange
        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Member>());

        var handler = new GetMembersQueryHandler(repoMock.Object);
        var query = new GetMembersQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
