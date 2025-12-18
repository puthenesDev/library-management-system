using LibraryManagement.Application.Contracts.Loans;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public sealed record BorrowBookCommand(
    Guid BookId,
    Guid MemberId
) : IRequest<LoanResponse>;