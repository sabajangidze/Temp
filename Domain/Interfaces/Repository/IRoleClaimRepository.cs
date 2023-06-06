using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain.Interfaces.Repository
{
    public interface IRoleClaimRepository : IBaseRepository<IdentityRoleClaim<string>>
    {
        Task<IdentityResult> InsertAsync(Role role, Claim claim);
        Task<IdentityRoleClaim<string>> OfIdAsync(int id);
        Task<IEnumerable<Claim>> GetRoleClaimsAsync(Role role);
    }
}