namespace LibraryManagement.Application.Contracts.Loans;
public sealed class BorrowBookRequest
{
    public Guid BookId { get; init; }
    public Guid MemberId { get; init; }
}