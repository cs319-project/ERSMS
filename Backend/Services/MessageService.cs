using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public MessageService(IMessageRepository messageRepository, IMapper mapper, IUserService userService)
        {
            _messageRepository = messageRepository;
            _userService = userService;
            _mapper = mapper;
        }

        // Methods
        public async Task<bool> AddMessage(CreateMessageDto createMessageDto)
        {
            var username = createMessageDto.SenderUsername;

            if (username == createMessageDto.RecipientUsername.ToLower())
                return false;

            var sender = await _userService.GetDomainUser(username);
            var recipient = await _userService.GetDomainUser(createMessageDto.RecipientUsername);

            if (recipient == null)
                return false;

            var message = new Message
            {
                // Sender = sender,
                // Recipient = recipient,
                SenderUsername = sender.IdentityUser.UserName,
                RecipientUsername = recipient.IdentityUser.UserName,
                Content = createMessageDto.Content
            };

            await _messageRepository.AddMessage(message);

            return true;
        }

        public async Task<bool> DeleteMessage(Guid id)
        {
            var message = await _messageRepository.GetMessage(id);

            if (message == null)
                return false;
            await _messageRepository.DeleteMessage(id);

            return true;
        }

        public async Task<IEnumerable<string>> GetDMUserList(string userName)
        {
            var user = await _userService.GetDomainUser(userName);

            if (user == null)
                return null;

            HashSet<string> userNameSet = new HashSet<string>();
            var messages = user.MessagesReceived.Concat(user.MessagesSent);

            foreach (var message in messages)
            {
                if (message.SenderUsername == userName)
                    userNameSet.Add(message.RecipientUsername);
                else
                    userNameSet.Add(message.SenderUsername);
            }

            return userNameSet.ToList();
        }

        public async Task<IEnumerable<MessageDto>> GetFullThread(string userName1, string userName2)
        {
            var user1 = await _userService.GetDomainUser(userName1);
            var user2 = await _userService.GetDomainUser(userName2);

            if (user1 == null || user2 == null)
                return null;

            var messagesFirstWay = await GetMessageThreadOneWay(userName1, userName2);
            var messagesSecondWay = await GetMessageThreadOneWay(userName2, userName1);

            // Concatinate two way in order of sent date
            var messages = messagesFirstWay.Concat(messagesSecondWay).OrderBy(m => m.MessageSent);
            return messages;
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadOneWay(string currentUserName, string recipientUserName)
        {
            var user = await _userService.GetDomainUser(currentUserName);
            var recipient = await _userService.GetDomainUser(recipientUserName);

            if (user == null || recipient == null)
                return null;

            var messages = await _messageRepository.GetMessageThreadOneWay(currentUserName, recipientUserName);

            return messages;
        }
    }
}
