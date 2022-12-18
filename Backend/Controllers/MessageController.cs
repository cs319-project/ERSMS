using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the Message API.</summary>
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="MessageController"/> class.</summary>
        /// <param name="messageService">The message service.</param>
        /// <param name="userService">The user service.</param>
        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        // Endpoints

        /// <summary>Creates a new message.</summary>
        /// <param name="createMessageDto">The message to create.</param>
        /// <returns>A <see cref="StatusCodeResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            if (await _messageService.AddMessage(createMessageDto))
            {
                return Ok();
            }
            return BadRequest("Failed to send message");
        }

        /// <summary>Gets the direct messaging thread between two users.</summary>
        /// <param name="sender">The sender's username.</param>
        /// <param name="recipient">The recipient's username.</param>
        /// <returns>The direct messaging thread between two users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetDirectMessagingOneWay([FromQuery] string sender, [FromQuery] string recipient)
        {
            var user1 = await _userService.GetDomainUser(sender);
            var user2 = await _userService.GetDomainUser(recipient);
            if (user1 == null || user2 == null) return BadRequest("Users not found");

            var messages = await _messageService.GetMessageThreadOneWay(sender, recipient);

            return messages != null || messages.Count() > 0 ? Ok(messages) : NotFound("No messages found");
        }

        /// <summary>Deletes a message.</summary>
        /// <param name="id">The ID of the message to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the message was deleted.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteMessage(Guid id)
        {
            var flag = await _messageService.DeleteMessage(id);
            return flag ? Ok() : BadRequest("Failed to delete message");
        }

        /// <summary>Retrieves the full thread of messages between two users.</summary>
        /// <param name="firstUser">The first user.</param>
        /// <param name="secondUser">The second user.</param>
        /// <returns>The full thread of messages between two users.</returns>
        [HttpGet("fullthread")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser([FromQuery] string firstUser, [FromQuery] string secondUser)
        {
            var user1 = await _userService.GetDomainUser(firstUser);
            var user2 = await _userService.GetDomainUser(secondUser);
            if (user1 == null || user2 == null) return BadRequest("Users not found");

            var messages = await _messageService.GetFullThread(firstUser, secondUser);

            return messages != null || messages.Count() > 0 ? Ok(messages) : NotFound("No messages found");
        }

        /// <summary>Gets the list of messages sent to a user.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of messages sent to a user.</returns>
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
