using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Repositories
{
    public class RoleClaimRepository : BaseRepository<IdentityRoleClaim<string>>, IRoleClaimRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleClaimRepository(ApplicationDbContext context, RoleManager<Role> roleManager)
            : base(context)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityRoleClaim<string>> OfIdAsync(int id)
        {
            return await _context.Set<IdentityRoleClaim<string>>().FindAsync(id);
        }

        public override Task InsertAsync(IdentityRoleClaim<string> aggregateRoot)
        {
            throw new NotSupportedException("Not supported method.");
        }

        public async Task<IdentityResult> InsertAsync(Role role, Claim claim)
        {
            return await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task<IEnumerable<Claim>> GetRoleClaimsAsync(Role role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }
    }
}