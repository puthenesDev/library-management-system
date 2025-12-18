using MediatR;

namespace LibraryManagement.Application.Members.Commands;

public record DeleteMemberCommand(Guid Id) : IRequest<bool>;