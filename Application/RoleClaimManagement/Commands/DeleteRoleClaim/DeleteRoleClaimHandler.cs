using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.RoleClaimManagement.Commands.DeleteRoleClaim
{
    public class DeleteRoleClaimHandler : IRequestHandler<DeleteRoleClaim>
    {
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleClaimHandler(IRoleClaimRepository roleClaimRepository, IUnitOfWork unitOfWork)
        {
            _roleClaimRepository = roleClaimRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteRoleClaim request, CancellationToken cancellationToken)
        {
            var roleClaim = await _roleClaimRepository.OfIdAsync(request.RoleClaimId);

            if (roleClaim == null)
            {
                throw new KeyNotFoundException($"Claim was not found for Id: {request.RoleClaimId}");
            }

            _roleClaimRepository.Delete(roleClaim);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}