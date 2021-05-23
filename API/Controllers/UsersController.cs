using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IPhotoServices photoService;

        public UsersController(
            IUserRepository userRepository, 
            IMapper mapper,
            IPhotoServices photoService)
        {
            this.mapper = mapper;
            this.photoService = photoService;
            this.userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var users = await this.userRepository.GetAllUsersAsync();
            var usersToReturn = this.mapper.Map<IEnumerable<MemberDTO>>(users);
            return Ok(usersToReturn);
        }


        [HttpGet]
        [Route("{username}", Name="GetUseryName")]
        public async Task<ActionResult<MemberDTO>> GetUserByName(string username)
        {
            var user = await userRepository.GetUserByNameAsync(username);
            var userToReturn = mapper.Map<MemberDTO>(user);
            return userToReturn;
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO){

            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByNameAsync(userName);

            mapper.Map(memberUpdateDTO, user);

            userRepository.update(user);

            if(await userRepository.SaveAllAsync()) 
                return NoContent();
            return BadRequest("Failed to save the changes");
        }


        [HttpPost]
        [Route("add-photo")]
        public async Task<ActionResult<Photo>> AddPhoto(IFormFile file){

            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByNameAsync(userName);

            var result = await photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo{
                url = result.SecureUrl.AbsolutePath,
                publicId = result.PublicId,
                isMain = user.photo.Count == 0 ? true: false,
            };

            user.photo.Add(photo);

            if(await userRepository.SaveAllAsync())

                return CreatedAtRoute(
                    "GetUseryName", 
                    new {userName = user.userName}, 
                    mapper.Map<Photo>(photo));

            return BadRequest("There is a problem in adding photo");

        }

        [HttpGet]
        [Route("set-main-photo/{photoId}")]
        public async Task<ActionResult> setMainPhoto(int photoId)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await this.userRepository.GetUserByNameAsync(userName);
            var photo = user.photo.FirstOrDefault(p => p.id == photoId);

            if (photo == null) return BadRequest("This photo id does not exist");

            if (photo.isMain) return BadRequest("This has already been set as the main picture");

            var previousMainPhoto = user.photo.FirstOrDefault(p => p.isMain == true);
            if (previousMainPhoto != null) previousMainPhoto.isMain = false;
            photo.isMain = true;

            await userRepository.SaveAllAsync();

            // return Ok($" photo id {photoId} has set as the the main");
            return Ok(JsonSerializer.Serialize($"Photo has been set as the main picture"));
        }



        // [HttpGet]
        // [Route("{id}")]
        // public async Task<ActionResult<AppUser>> GetUser(int id)
        // {
        //     return await userRepository.GetUserByIdAsync(id);
        // }


        // [HttpGet]
        // [Route("seed")]
        // public async Task<List<AppUser>> SeedData()
        // {
        //     return await Seed.SeedUsers(context);
        // }
    }
}