using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>A controller for handling announcements.</summary>
    public class AnnouncementController : BaseApiController
    {
        private readonly IAnnouncementService _announcementService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="AnnouncementController"/> class.</summary>
        /// <param name="announcementService">The announcement service.</param>
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // Endpoints

        /// <summary>Gets all announcements.</summary>
        /// <returns>A list of all announcements.</returns>
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

        /// <summary>Gets an announcement.</summary>
        /// <param name="id">The announcement's ID.</param>
        /// <returns>The announcement.</returns>
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

        /// <summary>Adds an announcement to the database.</summary>
        /// <param name="announcement">The announcement to add.</param>
        /// <returns>Returns the result of the add announcement operation.</returns>
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

        /// <summary>Updates an announcement.</summary>
        /// <param name="announcement">The announcement to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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

        /// <summary>Deletes an announcement.</summary>
        /// <param name="id">The ID of the announcement to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the announcement was deleted.</returns>
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
