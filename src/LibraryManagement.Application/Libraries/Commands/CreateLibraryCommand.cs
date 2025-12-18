using LibraryManagement.Application.Contracts.Libraries;
using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;

public sealed record CreateLibraryCommand(
    string Name,
    string Address
) : IRequest<LibraryResponse>;