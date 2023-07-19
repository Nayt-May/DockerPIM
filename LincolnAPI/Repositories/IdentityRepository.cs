using LincolnAPI.Database;
using LincolnAPI.DBModels.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LincolnAPI.Repositories
{
    public class IdentityRepository
    {
        private readonly PimDataAccess _context;

        public IdentityRepository(PimDataAccess context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        { 
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        { 
            var user = await _context.Users.Where(a=>a.UserName== userName).ToListAsync();
            if (user.Count == 0 || user.Count > 1)
            {
                return null;
            }
            return user.FirstOrDefault();
        }

        public async Task<User> InsertAsync(User user)
        { 
            return (await _context.Users.AddAsync(user)).Entity;
        }

        public async Task<User> UpdateAsync(User newUser)
        {
            return await Task.Run(() => {
                return _context.Users.Update(newUser).Entity;
                }
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> InsertRoleAsync(Role role)
        { 
            return (await _context.Roles.AddAsync(role)).Entity;
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await GetRoleAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role?> GetRoleAsync(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            return await Task.Run(() => {
                return _context.Roles.Update(role).Entity;
            }
           );
        }

        public async Task<IAsyncEnumerable<Role>> GetAllRolesAsync()
        {
            return await Task.Run(() =>
            {
                return _context.Roles.AsAsyncEnumerable();
            });
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
