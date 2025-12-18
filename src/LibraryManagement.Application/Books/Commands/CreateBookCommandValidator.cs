using FluentValidation;

namespace LibraryManagement.Application.Books.Commands;
public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.LibraryId).NotEmpty();

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Length(10, 13);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Author)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TotalCopies)
            .GreaterThan(0);
    }
}