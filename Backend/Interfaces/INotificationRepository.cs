using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the notification repository.</summary>
    public interface INotificationRepository
    {
        Task<bool> AddNotification(Notification notification);
        Task<bool> DeleteNotification(Guid id);
        Task<bool> UpdateNotification(Notification notification);
        Task<Notification> GetNotification(Guid id);
        Task<IEnumerable<Notification>> GetNotifications();
    }
}
