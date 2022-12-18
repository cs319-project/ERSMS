using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for notifications.</summary>
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="NotificationRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Adds a notification to the database.</summary>
        /// <param name="notification">The notification to add.</param>
        /// <returns>Whether the operation was successful.</returns>
        public async Task<bool> AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Deletes a notification.</summary>
        /// <param name="id">The ID of the notification to delete.</param>
        /// <returns>True if the notification was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a notification.</summary>
        /// <param name="id">The ID of the notification.</param>
        /// <returns>The notification.</returns>
        public async Task<Notification> GetNotification(Guid id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        /// <summary>Gets the notifications.</summary>
        /// <returns>The notifications.</returns>
        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        /// <summary>Updates a notification.</summary>
        /// <param name="notification">The notification to update.</param>
        /// <returns>Whether the update was successful.</returns>
        public async Task<bool> UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
