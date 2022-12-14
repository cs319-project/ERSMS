using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        // Constructor
        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        // Endpoints
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            if (await _messageService.AddMessage(createMessageDto))
            {
                return Ok();
            }
            return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetDirectMessagingOneWay([FromQuery] string sender, [FromQuery] string recipient)
        {
            var user1 = await _userService.GetDomainUser(sender);
            var user2 = await _userService.GetDomainUser(recipient);
            if (user1 == null || user2 == null) return BadRequest("Users not found");

            var messages = await _messageService.GetMessageThreadOneWay(sender, recipient);

            return messages != null || messages.Count() > 0 ? Ok(messages) : NotFound("No messages found");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            var flag = await _messageService.DeleteMessage(id);
            return flag ? Ok() : BadRequest("Failed to delete message");
        }

        [HttpGet("fullthread")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser([FromQuery] string firstUser, [FromQuery] string secondUser)
        {
            var user1 = await _userService.GetDomainUser(firstUser);
            var user2 = await _userService.GetDomainUser(secondUser);
            if (user1 == null || user2 == null) return BadRequest("Users not found");

            var messages = await _messageService.GetFullThread(firstUser, secondUser);

            return messages != null || messages.Count() > 0 ? Ok(messages) : NotFound("No messages found");
        }

        [HttpGet("userlist/{userName}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetUserList(string userName)
        {
            var user = await _userService.GetDomainUser(userName);
            if (user == null) return BadRequest("User not found");

            var messages = await _messageService.GetDMUserList(userName);

            return messages != null || messages.Count() > 0 ? Ok(messages) : NotFound("No messages found");
        }
    }
}
