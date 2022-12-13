using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DataContext _context;

        public AnnouncementRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> AddAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAnnouncement(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            _context.Announcements.Remove(announcement);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Announcement> GetAnnouncement(Guid id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<bool> UpdateAnnouncement(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
