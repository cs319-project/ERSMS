using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for announcements.</summary>
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DataContext _context;

        /// <summary>Creates a new instance of the <see cref="AnnouncementRepository"/> class.</summary>
        /// <param name="context">The data context to use.</param>
        public AnnouncementRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Adds an announcement to the database.</summary>
        /// <param name="announcement">The announcement to add.</param>
        /// <returns>Whether the announcement was added successfully.</returns>
        public async Task<bool> AddAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Deletes an announcement.</summary>
        /// <param name="id">The ID of the announcement to delete.</param>
        /// <returns>True if the announcement was deleted, false otherwise.</returns>
        public async Task<bool> DeleteAnnouncement(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            _context.Announcements.Remove(announcement);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets an announcement by its ID.</summary>
        /// <param name="id">The ID of the announcement.</param>
        /// <returns>The announcement with the specified ID.</returns>
        public async Task<Announcement> GetAnnouncement(Guid id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        /// <summary>Gets the announcements.</summary>
        /// <returns>The announcements.</returns>
        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            return await _context.Announcements.ToListAsync();
        }

        /// <summary>Updates an announcement.</summary>
        /// <param name="announcement">The announcement to update.</param>
        /// <returns>Whether the announcement was updated.</returns>
        public async Task<bool> UpdateAnnouncement(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
