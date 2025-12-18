using LibraryManagement.Application.Contracts.Loans;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public sealed class ReturnBookCommandHandler
    : IRequestHandler<ReturnBookCommand, LoanResponse>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;

    public ReturnBookCommandHandler(
        ILoanRepository loanRepository,
        IBookRepository bookRepository)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
    }

    public async Task<LoanResponse> Handle(
        ReturnBookCommand request,
        CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.LoanId, cancellationToken)
                   ?? throw new InvalidOperationException("Loan not found.");

        if (!loan.IsActive)
            throw new InvalidOperationException("Loan already returned.");

        var book = await _bookRepository.GetByIdAsync(loan.BookId, cancellationToken)
                   ?? throw new InvalidOperationException("Book not found.");

        loan.Return(DateTime.UtcNow);
        book.Return();

        await _loanRepository.SaveChangesAsync(cancellationToken);
        await _bookRepository.SaveChangesAsync(cancellationToken);

        return new LoanResponse
        {
            Id = loan.Id,
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            BorrowedAt = loan.BorrowedAt,
            ReturnedAt = loan.ReturnedAt,
            DueDate = loan.DueDate,
            IsActive = loan.IsActive
        };
    }
}