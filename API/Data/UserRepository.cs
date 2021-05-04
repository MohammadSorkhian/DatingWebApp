using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await context.Users
            .Include( u => u.photo )
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           return await context.Users.FirstOrDefaultAsync( u => u.Id == id);
        }

        public async Task<AppUser> GetUserByNameAsync(string name)
        {
            string _name = name.ToLower();

            return await context.Users
            .Include( u => u.photo )
            .SingleOrDefaultAsync( u => u.userName == _name);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}