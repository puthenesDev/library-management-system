using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Tests.Entities;

public class MemberTests
{
    [Fact]
    public void Member_SetsRegisteredAt()
    {
        var member = new Member("Alice", "alice@example.com");
        Assert.True(member.RegisteredAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Member_Throws_When_NameEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Member("", "test@example.com"));
    }
}