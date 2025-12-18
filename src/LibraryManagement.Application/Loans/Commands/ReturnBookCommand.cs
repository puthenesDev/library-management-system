using LibraryManagement.Application.Contracts.Loans;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public sealed record ReturnBookCommand(Guid LoanId) : IRequest<LoanResponse>;