using LibraryManagement.Application.Contracts.Members;
using MediatR;

namespace LibraryManagement.Application.Members.Queries;

public sealed record GetMemberByIdQuery(Guid Id) : IRequest<MemberResponse?>;