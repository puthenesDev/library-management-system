using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, bool>
{
    private readonly ILoanRepository _loanRepository;

    public DeleteLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<bool> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);
        if (loan == null) return false;

        await _loanRepository.DeleteAsync(loan, cancellationToken);
        await _loanRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}