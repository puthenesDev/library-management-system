using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Tests.Entities;

public class LoanTests
{
    [Fact]
    public void Loan_SetsDueDateCorrectly()
    {
        var now = DateTime.UtcNow;
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), now, TimeSpan.FromDays(7));

        Assert.Equal(now.AddDays(7), loan.DueDate);
        Assert.True(loan.IsActive);
    }

    [Fact]
    public void Return_SetsReturnedAt()
    {
        var now = DateTime.UtcNow;
        var loan = new Loan(Guid.NewGuid(), Guid.NewGuid(), now, TimeSpan.FromDays(7));

        loan.Return(DateTime.UtcNow);

        Assert.False(loan.IsActive);
        Assert.NotNull(loan.ReturnedAt);
    }
}