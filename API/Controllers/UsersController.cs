using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
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
        [Route("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserByName(string username)
        {
            var user = await userRepository.GetUserByNameAsync(username);
            var userToReturn = mapper.Map<MemberDTO>(user);
            return userToReturn;
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