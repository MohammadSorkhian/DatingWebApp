using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public MessageController( 
            IUserRepository userRepository, 
            IMessageRepository messageRepository, 
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<MessageDTO>> CreateMessage(CreateMessageDTO createMessg)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if ( userName == createMessg.recipientUsername.ToLower())
                return BadRequest("Can not send message to yourself");

            var sender = await userRepository.GetUserByNameAsync(userName);
            var recipient = await userRepository.GetUserByNameAsync(createMessg.recipientUsername);

            if(recipient == null) return NotFound("Could not find the user");

            var messg = new Message 
            {
                sender = sender,
                recipient = recipient,
                content = createMessg.content
            };

            messageRepository.AddMessage(messg);

            if (await messageRepository.SaveAllAsync()) 
                return Ok(this.mapper.Map<MessageDTO>(messg));

            return BadRequest("Could not send the message");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser(string container)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // if(currentUsername != userName) return BadRequest("You cannot access others messages");
            return Ok( await this.messageRepository
            .GetMessagesForUser(currentUsername, container));
        }


        [HttpGet]
        [Route("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessageThread(string username)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await messageRepository.GetMessageThread(currentUsername, username));
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Message mesg = await messageRepository.GetMessage(id);
            var m = mesg.sender.userName;
            var r = mesg.recipient.userName;
            var n = currentUsername;
            if(mesg.sender.userName != currentUsername && 
                mesg.recipient.userName != currentUsername) 
                return Unauthorized("This message does not belong to you");

            if(mesg.sender.userName == currentUsername) 
                mesg.senderDeleted = true;
            else
                mesg.recipientDeleted = true;
            
            if(mesg.senderDeleted && mesg.recipientDeleted)
                messageRepository.DeleteMessage(mesg);
            
            if(await messageRepository.SaveAllAsync())
                return Ok();

            return BadRequest("There is a problem in deleting the message");
        }

    }
}