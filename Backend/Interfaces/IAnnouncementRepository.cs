using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the announcement repository.</summary>
    public interface IAnnouncementRepository
    {
        Task<bool> AddAnnouncement(Announcement announcement);
        Task<IEnumerable<Announcement>> GetAnnouncements();
        Task<bool> DeleteAnnouncement(Guid id);
        Task<Announcement> GetAnnouncement(Guid id);
        Task<bool> UpdateAnnouncement(Announcement announcement);
    }
}
