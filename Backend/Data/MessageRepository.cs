using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for messages.</summary>
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="MessageRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userRepository">The user repository.</param>
        public MessageRepository(DataContext context, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>Adds a message to the database.</summary>
        /// <param name="message">The message to add.</param>
        /// <returns>Whether the message was added successfully.</returns>
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

        /// <summary>Deletes a message from the database.</summary>
        /// <param name="id">The ID of the message to delete.</param>
        /// <returns>True if the message was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteMessage(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a message.</summary>
        /// <param name="id">The ID of the message.</param>
        /// <returns>The message.</returns>
        public async Task<Message> GetMessage(Guid id)
        {
            return await _context.Messages.FindAsync(id);
        }

        /// <summary>Gets a message thread.</summary>
        /// <param name="senderUserName">The sender's user name.</param>
        /// <param name="recipientUserName">The recipient's user name.</param>
        /// <returns>The message thread.</returns>
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
