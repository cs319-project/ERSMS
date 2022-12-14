using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<bool> AddAnnouncement(Announcement announcement);
        Task<IEnumerable<Announcement>> GetAnnouncements();
        Task<bool> DeleteAnnouncement(Guid id);
        Task<Announcement> GetAnnouncement(Guid id);
        Task<bool> UpdateAnnouncement(Announcement announcement);
    }
}
