using MediatR;

namespace LibraryManagement.Application.Members.Commands;
public record UpdateMemberCommand(Guid Id, string Name, string Email) : IRequest<bool>;