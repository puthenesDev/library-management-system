using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;
public record DeleteLibraryCommand(Guid Id) : IRequest<bool>;