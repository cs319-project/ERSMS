using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>A controller for the EquivalenceRequest API.</summary>
    public class EquivalenceRequestController : BaseApiController
    {
        private readonly IEquivalenceRequestService _equivalenceRequestService;
        private readonly IUserService _userService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="EquivalenceRequestController"/> class.</summary>
        /// <param name="equivalenceRequestService">The equivalence request service.</param>
        /// <param name="userService">The user service.</param>
        public EquivalenceRequestController(IEquivalenceRequestService equivalenceRequestService, IUserService userService)
        {
            _equivalenceRequestService = equivalenceRequestService;
            _userService = userService;
        }

        // Endpoints

        /// <summary>Submits an equivalence request.</summary>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<EquivalenceRequestDto>> SubmitEquivalenceForm([FromForm] string studentId,
                                                                [FromForm] string hostCourseName,
                                                                [FromForm] string hostCourseCode,
                                                                [FromForm] string additionalNotes,
                                                                [FromForm] double hostCourseECTS,
                                                                [FromForm] int exemptedCourseCredit,
                                                                [FromForm] double exemptedCourseECTS,
                                                                [FromForm] string exemptedCourseCode,
                                                                [FromForm] string exemptedCourseName,
                                                                [FromForm] string exemptedCourseType,
                                                                [FromForm] IFormFile Syllabus)
        {
            if (Syllabus == null)
            {
                return BadRequest("No file uploaded");
            }

            if (Path.GetExtension(Syllabus.FileName) == ".pdf"
                    || Path.GetExtension(Syllabus.FileName) == ".docx")
            {
                EquivalenceRequestDto request = new EquivalenceRequestDto
                {
                    StudentId = studentId,
                    HostCourseECTS = hostCourseECTS,
                    HostCourseName = hostCourseName,
                    HostCourseCode = hostCourseCode,
                    SubmissionDate = DateTime.Now,
                    AdditionalNotes = additionalNotes,
                    InstructorApproval = null,
                    FileName = Syllabus.FileName
                };
                request.ExemptedCourse = new ExemptedCourseDto
                {
                    BilkentCredits = exemptedCourseCredit,
                    ECTS = exemptedCourseECTS,
                    CourseCode = exemptedCourseCode,
                    CourseName = exemptedCourseName,
                    CourseType = exemptedCourseType
                };

                return await _equivalenceRequestService.AddEquivalenceRequestToStudent(request, Syllabus)
                                ? Ok(request) : BadRequest("Failed to add Equivalence Request");
            }
            return BadRequest("Wrong formated syllabus");
        }

        /// <summary>Downloads a syllabus.</summary>
        /// <param name="id">The ID of the syllabus.</param>
        /// <returns>The syllabus as a file.</returns>
        [HttpGet("download/{id:Guid}")]
        public async Task<ActionResult> DownloadSyllabus(Guid id)
        {
            var result = await _equivalenceRequestService.DownloadSyllabus(id);

            if (Path.GetExtension(result.Item2) == ".pdf")
            {
                return (result != (null, null))
                            ? File(result.Item1, "application/pdf", result.Item2)
                            : NotFound();
            }
            else if (Path.GetExtension(result.Item2) == ".docx")
            {
                return (result != (null, null))
                            ? File(result.Item1, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", result.Item2)
                            : NotFound();
            }
            else
            {
                return BadRequest("Wrong file format");
            }
        }

        /// <summary>Gets all the equivalence requests.</summary>
        /// <returns>The equivalence requests.</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetEquivalenceRequests();
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets an equivalence request.</summary>
        /// <param name="id">The ID of the equivalence request.</param>
        /// <returns>The equivalence request.</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EquivalenceRequestDto>> GetEquivalenceRequest(Guid id)
        {
            var equivalenceRequest = await _equivalenceRequestService.GetEquivalenceRequest(id);
            if (equivalenceRequest == null)
            {
                return NotFound();
            }
            return Ok(equivalenceRequest);
        }

        /// <summary>Deletes an Equivalence Request.</summary>
        /// <param name="id">The ID of the Equivalence Request to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteEquivalenceRequest(Guid id)
        {
            if (await _equivalenceRequestService.DeleteEquivalenceRequest(id))
            {
                return Ok(true);
            }
            return BadRequest("Failed to delete Equivalence Request");
        }

        /// <summary>Updates an Equivalence Request.</summary>
        /// <param name="equivalenceRequest">The Equivalence Request to update.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateEquivalenceRequest(EquivalenceRequestDto equivalenceRequest)
        {
            if (await _equivalenceRequestService.UpdateEquivalenceRequest(equivalenceRequest))
            {
                return Ok(true);
            }
            return BadRequest("Failed to update Equivalence Request");
        }

        /// <summary>Updates the Syllabus of an Equivalence Request.</summary>
        /// <param name="id">The ID of the Equivalence Request.</param>
        /// <param name="Syllabus">The Syllabus file.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPatch("syllabus/{id:Guid}")]
        public async Task<ActionResult> UpdateSyllabus(Guid id, IFormFile Syllabus)
        {
            if (Syllabus == null)
            {
                return BadRequest("No file uploaded");
            }

            if (Path.GetExtension(Syllabus.FileName) == ".pdf"
                    || Path.GetExtension(Syllabus.FileName) == ".docx")
            {
                return await _equivalenceRequestService.UpdateEquivalenceRequestSyllabus(id, Syllabus)
                                ? Ok(true) : BadRequest("Failed to update Syllabus");
            }
            return BadRequest("Wrong formated syllabus");
        }

        /// <summary>Gets all archived equivalence requests.</summary>
        /// <returns>All archived equivalence requests.</returns>
        [HttpGet("archived/all")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequests();
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets all non-archived equivalence requests.</summary>
        /// <returns>All non-archived equivalence requests.</returns>
        [HttpGet("nonarchived/all")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequests();
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Retrieves the archived equivalence requests for a department.</summary>
        /// <param name="userName">The user name of the coordinator.</param>
        /// <returns>The archived equivalence requests for the department.</returns>
        [HttpGet("archived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets non-archived equivalence requests by department.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The non-archived equivalence requests by department.</returns>
        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Retrieves the archived equivalence requests for a course.</summary>
        /// <param name="courseCode">The course code.</param>
        /// <returns>The archived equivalence requests for the course.</returns>
        [HttpGet("archived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequestsByCourseCode(courseCode);
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets all non-archived equivalence requests for a course.</summary>
        /// <param name="courseCode">The course code.</param>
        /// <returns>A list of non-archived equivalence requests for the course.</returns>
        [HttpGet("nonarchived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequestsByCourseCode(courseCode);
            if (equivalenceRequests == null || equivalenceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets the equivalence requests of a student.</summary>
        /// <param name="studentID">The ID of the student.</param>
        /// <returns>The equivalence requests of the student.</returns>
        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestsOfStudent(string studentID)
        {
            var equivalenceRequests = await _equivalenceRequestService.GetEquivalenceRequestsOfStudent(studentID);
            if (equivalenceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets the equivalence requests by department for coordinator.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The equivalence requests by department for coordinator.</returns>
        [HttpGet("department/{userName}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            var equivalenceRequests = await _equivalenceRequestService.GetEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalenceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Gets all equivalence requests for a course.</summary>
        /// <param name="courseCode">The course code.</param>
        /// <returns>A collection of equivalence requests.</returns>
        [HttpGet("course/{courseCode}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestByCourseCode(string courseCode)
        {
            var equivalenceRequests = await _equivalenceRequestService.GetEquivalenceRequestByCourseCode(courseCode);
            if (equivalenceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalenceRequests);
        }

        /// <summary>Approves a request.</summary>
        /// <param name="requestId">The request ID.</param>
        /// <param name="approval">The approval.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("approve/{requestId:Guid}")]
        public async Task<ActionResult> ApproveRequest(Guid requestId, ApprovalDto approval)
        {
            if (await _equivalenceRequestService.ApproveRequest(requestId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve request");
        }

        /// <summary>Cancels an equivalence request.</summary>
        /// <param name="requestId">The ID of the request to cancel.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPatch("cancel/{requestId:Guid}")]
        public async Task<ActionResult> CancelRequest(Guid requestId)
        {
            if (await _equivalenceRequestService.CancelEquivalenceRequest(requestId))
            {
                return Ok(true);
            }
            return BadRequest("Failed to cancel request");
        }
    }
}
