using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    /// <summary>An interface for the announcement service.</summary>
    public interface IAnnouncementService
    {
        Task<bool> AddAnnouncement(AnnouncementDto announcement);
        Task<IEnumerable<AnnouncementDto>> GetAnnouncements();
        Task<bool> DeleteAnnouncement(Guid id);
        Task<AnnouncementDto> GetAnnouncement(Guid id);
        Task<bool> UpdateAnnouncement(AnnouncementDto announcement);
    }
}
