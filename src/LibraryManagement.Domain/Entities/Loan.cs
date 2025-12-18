namespace LibraryManagement.Domain.Entities;
public class Loan
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime BorrowedAt { get; private set; }
    public DateTime? ReturnedAt { get; private set; }
    public DateTime DueDate { get; private set; }

    public Book Book { get; private set; } = null!;
    public Member Member { get; private set; } = null!;

    private Loan() { }

    public Loan(Guid bookId, Guid memberId, DateTime now, TimeSpan duration)
    {
        if (bookId == Guid.Empty) throw new ArgumentException("BookId required");
        if (memberId == Guid.Empty) throw new ArgumentException("MemberId required");

        Id = Guid.NewGuid();
        BookId = bookId;
        MemberId = memberId;
        BorrowedAt = now;
        DueDate = now.Add(duration);
    }

    public void Return(DateTime now)
    {
        if (ReturnedAt.HasValue)
            throw new InvalidOperationException("Loan already returned.");

        ReturnedAt = now;
    }

    public void UpdateDueDate(DateTime newDueDate)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update a returned loan.");

        if (newDueDate <= BorrowedAt)
            throw new ArgumentException("Due date must be after borrowed date.");

        DueDate = newDueDate;
    }

    public bool IsActive => !ReturnedAt.HasValue;
}
