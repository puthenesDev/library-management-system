using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public record UpdateLoanCommand(Guid Id, DateTime DueDate) : IRequest<bool>;