using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetMessageThreadOneWay(string currentUserName, string recipientUserName);
        Task<bool> AddMessage(CreateMessageDto message);
        Task<bool> DeleteMessage(Guid id);
        Task<IEnumerable<MessageDto>> GetFullThread(string userName1, string userName2);
        Task<IEnumerable<string>> GetDMUserList(string userName);
    }
}
