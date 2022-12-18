using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the message repository.</summary>
    public interface IMessageRepository
    {
        Task<bool> AddMessage(Message message);
        Task<bool> DeleteMessage(Guid id);
        Task<Message> GetMessage(Guid id);
        Task<IEnumerable<MessageDto>> GetMessageThreadOneWay(string senderUserName, string recipientUserName);
    }
}
