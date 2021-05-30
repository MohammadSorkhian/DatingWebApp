using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : IlikesRepository
    {
        private readonly DataContext context;
        public LikesRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await context.UserLikes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<IEnumerable<LikeDTO>> GetUserLikes(string predicate, int userId)
        {
            var users = Enumerable.Empty<AppUser>().AsQueryable();

            if (predicate == "liked")
            {
                var likes = context.UserLikes.Where(ul => ul.sourceUserId == userId);
                users = likes.Select(l => l.likedUser);
            }

            if (predicate == "likedBy")
            {
                var likes = context.UserLikes.Where(ul => ul.likedUserId == userId);
                users = likes.Select(l => l.sourceUser);
            }

            return await users.Select(AppUser => new LikeDTO
            {
                Id = AppUser.Id,
                userName = AppUser.userName,
                age = AppUser.GetAge(),
                knownAs = AppUser.knownAs,
                photoUrl = AppUser.photo.FirstOrDefault(p => p.isMain == true).url,
                city = AppUser.city
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await context.Users
                .Include(u => u.likedUsers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}