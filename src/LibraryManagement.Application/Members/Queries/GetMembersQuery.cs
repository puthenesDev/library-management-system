using LibraryManagement.Application.Contracts.Members;
using MediatR;

namespace LibraryManagement.Application.Members.Queries;

public record GetMembersQuery() : IRequest<IReadOnlyList<MemberResponse>>;