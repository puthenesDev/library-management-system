using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Members.Commands;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member == null) return false;

        member.UpdateDetails(request.Name, request.Email); // domain method
        await _memberRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}