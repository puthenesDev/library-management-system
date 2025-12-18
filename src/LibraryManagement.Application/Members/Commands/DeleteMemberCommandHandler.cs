using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Members.Commands;
public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository;

    public DeleteMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member == null) return false;

        await _memberRepository.DeleteAsync(member, cancellationToken);
        await _memberRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}