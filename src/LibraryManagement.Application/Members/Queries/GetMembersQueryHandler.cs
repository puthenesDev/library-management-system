using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Members.Queries;

public class GetMembersQueryHandler
       : IRequestHandler<GetMembersQuery, IReadOnlyList<MemberResponse>>
{
    private readonly IMemberRepository _memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IReadOnlyList<MemberResponse>> Handle(
        GetMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken);

        var responses = members
            .Select(m => new MemberResponse
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email
            })
            .ToList();

        return responses;
    }
}