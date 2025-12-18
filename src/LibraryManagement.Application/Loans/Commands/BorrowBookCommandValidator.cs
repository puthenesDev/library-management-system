using FluentValidation;

namespace LibraryManagement.Application.Loans.Commands;
public sealed class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
{
    public BorrowBookCommandValidator()
    {
        RuleFor(x => x.BookId).NotEmpty();
        RuleFor(x => x.MemberId).NotEmpty();
    }
}