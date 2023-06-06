using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(ApplicationDbContext context, RoleManager<Role> roleManager)
            : base(context)
        {
            _roleManager = roleManager;
        }

        public override Task InsertAsync(Role aggregateRoot)
        {
            throw new NotSupportedException("Not supported method.");
        }

        public async Task<IdentityResult> CreateAsync(Role role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(Role role)
        {
            var result = await _roleManager.UpdateAsync(role);

            return result;
        }

        public override void Update(Role aggregateRoot)
        {
            _ = UpdateAsync(aggregateRoot).Result;
        }

        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);

            return result;
        }

        public override void Delete(Role aggregateRoot)
        {
            _ = DeleteAsync(aggregateRoot).Result;
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}