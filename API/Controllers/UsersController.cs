using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;
        public UsersController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return await this.context.Users.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await this.context.Users.FirstOrDefaultAsync( u => u.Id == id);
        }
    }
}