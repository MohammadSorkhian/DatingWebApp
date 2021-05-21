using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.context = context;
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO regDTO)
        {

            if (await UserExists(regDTO.userName)) return BadRequest("The user exists");

            using var hmac = new HMACSHA512();

            var newUser = new AppUser()
            {
                userName = regDTO.userName.ToLower(),
                passWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regDTO.passWord)),
                passWordSalt = hmac.Key,
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return new UserDTO
            {
                userName = newUser.userName,
                token = tokenService.CreateToken(newUser)
            };
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loingDTO)
        {

            AppUser user = await context.Users
            .Include("photo")
            .FirstOrDefaultAsync(x => x.userName == loingDTO.userName);

            if (user == null) { return Unauthorized("Invalid Username"); }

            using var hmac = new HMACSHA512(user.passWordSalt);

            var computeHasg = hmac.ComputeHash(Encoding.UTF8.GetBytes(loingDTO.passWord));

            for (int i = 0; i < computeHasg.Length; i++)
            {
                if (computeHasg[i] != user.passWordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }


            return new UserDTO
            {
                userName = user.userName,
                token = tokenService.CreateToken(user),
                photoUrl = user.photo.FirstOrDefault( p => p.isMain == true)?.url
            };
        }

        public async Task<bool> UserExists(string userName)
        {
            return await context.Users.AnyAsync(x => x.userName == userName.ToLower());
        }

    }
}