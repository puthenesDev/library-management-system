namespace LibraryManagement.Application.Contracts.Loans;
public sealed class LoanResponse
{
    public Guid Id { get; init; }
    public Guid BookId { get; init; }
    public Guid MemberId { get; init; }
    public DateTime BorrowedAt { get; init; }
    public DateTime? ReturnedAt { get; init; }
    public DateTime DueDate { get; init; }
    public bool IsActive { get; init; }
}
