using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task<List<AppUser>> SeedUsers (DataContext context)
        {
            if(await context.Users.AnyAsync()) return context.Users.ToList<AppUser>();

            var userData = await File.ReadAllTextAsync("./Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users){
                user.userName = user.userName.ToLower();
                var hmac = new HMACSHA512();
                user.passWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$W0rd"));
                user.passWordSalt = hmac.Key;
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
            return context.Users.ToList<AppUser>();
        }
    }
}