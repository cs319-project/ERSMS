using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        // Constructor
        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Notification> GetNotification(Guid id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
