using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class EquivalenceRequestController : BaseApiController
    {
        private readonly IEquivalenceRequestService _equivalenceRequestService;
        private readonly IUserService _userService;

        // Constructor
        public EquivalenceRequestController(IEquivalenceRequestService equivalenceRequestService, IUserService userService)
        {
            _equivalenceRequestService = equivalenceRequestService;
            _userService = userService;
        }

        // Endpoints
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
                                ? Ok(request) : BadRequest("Failed to add Equivalance Request");
            }
            return BadRequest("Wrong formated syllabus");
        }

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

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetEquivalenceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EquivalenceRequestDto>> GetEquivalenceRequest(Guid id)
        {
            var equivalanceRequest = await _equivalenceRequestService.GetEquivalenceRequest(id);
            if (equivalanceRequest == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequest);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteEquivalenceRequest(Guid id)
        {
            if (await _equivalenceRequestService.DeleteEquivalenceRequest(id))
            {
                return Ok(true);
            }
            return BadRequest("Failed to delete Equivalance Request");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEquivalenceRequest(EquivalenceRequestDto equivalanceRequest)
        {
            if (await _equivalenceRequestService.UpdateEquivalenceRequest(equivalanceRequest))
            {
                return Ok(true);
            }
            return BadRequest("Failed to update Equivalance Request");
        }

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

        [HttpGet("archived/all")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/all")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("archived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("archived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetArchivedEquivalenceRequestsByCourseCode(courseCode);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalenceRequestDto>>> GetNonArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await _equivalenceRequestService.GetNonArchivedEquivalenceRequestsByCourseCode(courseCode);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestsOfStudent(string studentID)
        {
            var equivalanceRequests = await _equivalenceRequestService.GetEquivalenceRequestsOfStudent(studentID);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("department/{userName}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            var equivalanceRequests = await _equivalenceRequestService.GetEquivalenceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("course/{courseCode}")]
        public async Task<ActionResult<ICollection<EquivalenceRequestDto>>> GetEquivalenceRequestByCourseCode(string courseCode)
        {
            var equivalanceRequests = await _equivalenceRequestService.GetEquivalenceRequestByCourseCode(courseCode);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpPost("approve/{requestId:Guid}")]
        public async Task<ActionResult> ApproveRequest(Guid requestId, ApprovalDto approval)
        {
            if (await _equivalenceRequestService.ApproveRequest(requestId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve request");
        }

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
