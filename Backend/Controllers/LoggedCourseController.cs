using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class LoggedCourseController : BaseApiController
    {
        private readonly ILoggedCourseService _loggedCourseService;

        // Constructor
        public LoggedCourseController(ILoggedCourseService loggedCourseService)
        {
            _loggedCourseService = loggedCourseService;
        }

        // Endpoints
        [HttpGet("logged-equivalent-courses")]
        public async Task<ActionResult<IEnumerable<LoggedEquivalantCourseDto>>> GetLoggedEquivalentCourses()
        {
            var loggedEquivalantCourses = await _loggedCourseService.GetLoggedEquivalantCourses();
            return loggedEquivalantCourses == null || loggedEquivalantCourses.Count() <= 0
                        ? NotFound() : Ok(loggedEquivalantCourses);
        }

        [HttpGet("logged-transferred-courses")]
        public async Task<ActionResult<IEnumerable<LoggedTransferredCourseDto>>> GetLoggedTransferredCourses()
        {
            var loggedTransferredCourses = await _loggedCourseService.GetLoggedTransferredCourses();
            return loggedTransferredCourses == null || loggedTransferredCourses.Count() <= 0
                        ? NotFound() : Ok(loggedTransferredCourses);
        }

        [HttpDelete("logged-equivalent-courses/{id:guid}")]
        public async Task<ActionResult> DeleteLoggedEquivalentCourse(Guid id)
        {
            return await _loggedCourseService.DeleteLoggedEquivalantCourse(id)
                        ? Ok() : BadRequest();
        }

        [HttpDelete("logged-transferred-courses/{id:guid}")]
        public async Task<ActionResult> DeleteLoggedTransferredCourse(Guid id)
        {
            return await _loggedCourseService.DeleteLoggedTransferredCourse(id)
                        ? Ok() : BadRequest();
        }
    }
}
