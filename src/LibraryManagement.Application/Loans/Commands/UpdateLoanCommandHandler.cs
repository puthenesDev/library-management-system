using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Loans.Commands;
public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, bool>
{
    private readonly ILoanRepository _loanRepository;

    public UpdateLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<bool> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);
        if (loan == null) return false;

        loan.UpdateDueDate(request.DueDate); 
        await _loanRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}