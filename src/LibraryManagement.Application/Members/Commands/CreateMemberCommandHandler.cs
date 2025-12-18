using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Members.Commands;

public sealed class CreateMemberCommandHandler
    : IRequestHandler<CreateMemberCommand, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberResponse> Handle(
        CreateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var member = new Member(request.Name, request.Email);

        await _memberRepository.AddAsync(member, cancellationToken);
        await _memberRepository.SaveChangesAsync(cancellationToken);

        return new MemberResponse
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            RegisteredAt = member.RegisteredAt
        };
    }
}