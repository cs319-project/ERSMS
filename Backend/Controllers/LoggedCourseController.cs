using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>A controller for the logged courses.</summary>
    public class LoggedCourseController : BaseApiController
    {
        private readonly ILoggedCourseService _loggedCourseService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="LoggedCourseController"/> class.</summary>
        /// <param name="loggedCourseService">The logged course service.</param>
        public LoggedCourseController(ILoggedCourseService loggedCourseService)
        {
            _loggedCourseService = loggedCourseService;
        }

        // Endpoints

        /// <summary>Gets the logged equivalent courses.</summary>
        /// <returns>The logged equivalent courses.</returns>
        [HttpGet("logged-equivalent-courses")]
        public async Task<ActionResult<IEnumerable<LoggedEquivalentCourseDto>>> GetLoggedEquivalentCourses()
        {
            var loggedEquivalantCourses = await _loggedCourseService.GetLoggedEquivalantCourses();
            return loggedEquivalantCourses == null || loggedEquivalantCourses.Count() <= 0
                        ? NotFound() : Ok(loggedEquivalantCourses);
        }

        /// <summary>Gets the logged transferred courses.</summary>
        /// <returns>The logged transferred courses.</returns>
        [HttpGet("logged-transferred-courses")]
        public async Task<ActionResult<IEnumerable<LoggedTransferredCourseDto>>> GetLoggedTransferredCourses()
        {
            var loggedTransferredCourses = await _loggedCourseService.GetLoggedTransferredCourses();
            return loggedTransferredCourses == null || loggedTransferredCourses.Count() <= 0
                        ? NotFound() : Ok(loggedTransferredCourses);
        }

        /// <summary>Deletes a logged equivalent course.</summary>
        /// <param name="id">The ID of the logged equivalent course to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the logged equivalent course was deleted.</returns>
        [HttpDelete("logged-equivalent-courses/{id:guid}")]
        public async Task<ActionResult> DeleteLoggedEquivalentCourse(Guid id)
        {
            return await _loggedCourseService.DeleteLoggedEquivalantCourse(id)
                        ? Ok() : BadRequest();
        }

        /// <summary>Deletes a logged transferred course.</summary>
        /// <param name="id">The ID of the logged transferred course to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the logged transferred course was deleted.</returns>
        [HttpDelete("logged-transferred-courses/{id:guid}")]
        public async Task<ActionResult> DeleteLoggedTransferredCourse(Guid id)
        {
            return await _loggedCourseService.DeleteLoggedTransferredCourse(id)
                        ? Ok() : BadRequest();
        }
    }
}
