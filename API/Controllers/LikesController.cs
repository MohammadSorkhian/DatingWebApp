using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Extentions;
using System.Security.Claims;
using API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IlikesRepository likesRepository;
        public LikesController(IUserRepository userRepository, IlikesRepository likesRepository)
        {
            this.likesRepository = likesRepository;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("{userName}")]
        public async Task<ActionResult> AddLike(string userName)
        {
            var sourceUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sourceUser = await userRepository.GetUserByNameAsync(sourceUserName);
            var likedUser = await userRepository.GetUserByNameAsync(userName);

            if (likedUser == null) return NotFound();

            if (sourceUser.userName == userName) return BadRequest();

            var userLike = await likesRepository.GetUserLike(sourceUser.Id, likedUser.Id);

            if(userLike != null) return BadRequest("You have already Liked this user");

            userLike = new UserLike()
            {
                sourceUserId = sourceUser.Id,
                likedUserId = likedUser.Id,
            };

            sourceUser.likedUsers.Add(userLike);

            if(await userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed To Add the user to your liked list");

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDTO>>> GetUserLikes (string predicate){

            var sourceUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sourceUser = await userRepository.GetUserByNameAsync(sourceUserName);

            var users = await likesRepository.GetUserLikes(predicate, sourceUser.Id);

            return Ok(users);
            
        }



    }
}