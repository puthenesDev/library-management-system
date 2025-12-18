using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;

public record UpdateLibraryCommand(Guid Id, string Name, string Address) : IRequest<bool>;