using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IdentityResult> InsertAsync(User user, string password, string roleName);
        Task<IdentityResult> AddToRoleAsync(User user, Role role);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task<(bool success, User? user)> ValidateUserAsync(string userName, string password);
        Task<IList<string>> GetUserRolesAsync(User user);
    }
}