using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Members.Queries;

public sealed class GetMemberByIdQueryHandler
    : IRequestHandler<GetMemberByIdQuery, MemberResponse?>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberResponse?> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null) return null;

        return new MemberResponse
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            RegisteredAt = member.RegisteredAt
        };
    }
}