using LibraryManagement.Application.Contracts.Members;
using MediatR;

namespace LibraryManagement.Application.Members.Commands;

public sealed record CreateMemberCommand(
    string Name,
    string Email
) : IRequest<MemberResponse>;
