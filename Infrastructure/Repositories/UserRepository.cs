using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
            : base(context)
        {
            _userManager = userManager;
        }

        public override Task InsertAsync(User aggregateRoot)
        {
            throw new NotSupportedException("Not supported method.");
        }

        public async Task<IdentityResult> InsertAsync(User user, string password, string roleName)
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, roleName);

            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, Role role)
        {
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            return result;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public override void Update(User aggregateRoot)
        {
            _ = UpdateAsync(aggregateRoot).Result;
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public override void Delete(User aggregateRoot)
        {
            _ = DeleteAsync(aggregateRoot).Result;
        }

        public async Task<(bool success, User user)> ValidateUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                return (result, user);
            }

            return (false, null);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}