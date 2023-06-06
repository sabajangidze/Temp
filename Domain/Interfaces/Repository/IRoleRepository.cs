using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> UpdateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<Role> FindByNameAsync(string roleName);
    }
}