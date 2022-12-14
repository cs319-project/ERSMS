using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class AnnouncementController : BaseApiController
    {
        private readonly IAnnouncementService _announcementService;

        // Constructor
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // Endpoints
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements()
        {
            var announcements = await _announcementService.GetAnnouncements();

            if (announcements.Count() == 0 || announcements == null)
            {
                return NotFound("No announcements found");
            }

            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementService.GetAnnouncement(id);

            if (announcement == null)
            {
                return NotFound("Announcement not found");
            }

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<ActionResult> AddAnnouncement(AnnouncementDto announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement is null");
            }

            // No Validator needed
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest("Invalid model object");
            // }

            var result = await _announcementService.AddAnnouncement(announcement);

            if (!result)
            {
                return BadRequest("Announcement could not be added");
            }

            return Ok("Announcement added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAnnouncement(AnnouncementDto announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement is null");
            }

            var result = await _announcementService.UpdateAnnouncement(announcement);

            if (!result)
            {
                return BadRequest("Announcement could not be updated");
            }

            return Ok("Announcement updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnnouncement(Guid id)
        {
            var result = await _announcementService.DeleteAnnouncement(id);

            if (!result)
            {
                return BadRequest("Announcement could not be deleted");
            }

            return Ok("Announcement deleted");
        }
    }
}
