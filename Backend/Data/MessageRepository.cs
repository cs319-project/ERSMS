using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddMessage(Message message)
        {
            _context.Messages.Add(message);
            // add message to recipient and sender
            var recipient = await _userRepository.GetDomainUser(message.RecipientUsername);
            var sender = await _userRepository.GetDomainUser(message.SenderUsername);
            if (recipient == null || sender == null) return false;
            recipient.MessagesReceived.Add(message);
            sender.MessagesSent.Add(message);
            // update users
            await _userRepository.UpdateDomainUser(recipient);
            await _userRepository.UpdateDomainUser(sender);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMessage(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(Guid id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadOneWay(string senderUserName, string recipientUserName)
        {
            var query = _context.Messages
                .Where(m => m.RecipientUsername == recipientUserName
                            && m.SenderUsername == senderUserName
                )
                .OrderBy(m => m.MessageSent)
                .AsQueryable();

            return await query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
