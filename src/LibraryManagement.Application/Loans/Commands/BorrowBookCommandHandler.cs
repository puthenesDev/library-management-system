using LibraryManagement.Application.Contracts.Loans;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public sealed class BorrowBookCommandHandler
    : IRequestHandler<BorrowBookCommand, LoanResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly ILoanRepository _loanRepository;

    public BorrowBookCommandHandler(
        IBookRepository bookRepository,
        IMemberRepository memberRepository,
        ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
        _loanRepository = loanRepository;
    }

    public async Task<LoanResponse> Handle(
        BorrowBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken)
                   ?? throw new InvalidOperationException("Book not found.");

        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken)
                     ?? throw new InvalidOperationException("Member not found.");

        book.Borrow();

        var now = DateTime.UtcNow;
        var loan = new Loan(book.Id, member.Id, now, TimeSpan.FromDays(14));

        await _loanRepository.AddAsync(loan, cancellationToken);
        await _loanRepository.SaveChangesAsync(cancellationToken);

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