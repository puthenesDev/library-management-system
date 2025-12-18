using MediatR;

namespace LibraryManagement.Application.Loans.Commands;
public record DeleteLoanCommand(Guid Id) : IRequest<bool>;