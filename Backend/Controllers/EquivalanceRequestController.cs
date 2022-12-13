using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class EquivalanceRequestController : BaseApiController
    {
        private readonly IEquivalanceRequestService _equivalanceRequestService;
        private readonly IUserService _userService;

        // Constructor
        public EquivalanceRequestController(IEquivalanceRequestService equivalanceRequestService, IUserService userService)
        {
            _equivalanceRequestService = equivalanceRequestService;
            _userService = userService;
        }

        // Endpoints
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetEquivalanceRequests()
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetEquivalanceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EquivalanceRequestDto>> GetEquivalanceRequest(Guid id)
        {
            var equivalanceRequest = await _equivalanceRequestService.GetEquivalanceRequest(id);
            if (equivalanceRequest == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequest);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteEquivalanceRequest(Guid id)
        {
            if (await _equivalanceRequestService.DeleteEquivalanceRequest(id))
            {
                return Ok(true);
            }
            return BadRequest("Failed to delete Equivalance Request");
        }

        [HttpGet("archived/all")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetArchivedEquivalanceRequests()
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetArchivedEquivalanceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/all")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetNonArchivedEquivalanceRequests()
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetNonArchivedEquivalanceRequests();
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("archived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetArchivedEquivalanceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetArchivedEquivalanceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetNonArchivedEquivalanceRequestsByDepartment(string userName)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetNonArchivedEquivalanceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("archived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetArchivedEquivalanceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetArchivedEquivalanceRequestsByCourseCode(courseCode);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("nonarchived/course/{courseCode}")]
        public async Task<ActionResult<IEnumerable<EquivalanceRequestDto>>> GetNonArchivedEquivalanceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await _equivalanceRequestService.GetNonArchivedEquivalanceRequestsByCourseCode(courseCode);
            if (equivalanceRequests == null || equivalanceRequests.Count() == 0)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<ICollection<EquivalanceRequestDto>>> GetEquivalanceRequestsOfStudent(string studentID)
        {
            var equivalanceRequests = await _equivalanceRequestService.GetEquivalanceRequestsOfStudent(studentID);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("department/{userName}")]
        public async Task<ActionResult<ICollection<EquivalanceRequestDto>>> GetEquivalanceRequestsByDepartmentForCoordinator(string userName)
        {
            var equivalanceRequests = await _equivalanceRequestService.GetEquivalanceRequestsByDepartmentForCoordinator(userName);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpGet("course/{courseCode}")]
        public async Task<ActionResult<ICollection<EquivalanceRequestDto>>> GetEquivalanceRequestByCourseCode(string courseCode)
        {
            var equivalanceRequests = await _equivalanceRequestService.GetEquivalanceRequestByCourseCode(courseCode);
            if (equivalanceRequests == null)
            {
                return NotFound();
            }
            return Ok(equivalanceRequests);
        }

        [HttpPost("approve/{requestId:Guid}")]
        public async Task<ActionResult> ApproveRequest(Guid requestId, ApprovalDto approval)
        {
            if (await _equivalanceRequestService.ApproveRequest(requestId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve request");
        }

        [HttpPatch("cancel/{requestId:Guid}")]
        public async Task<ActionResult> CancelRequest(Guid requestId)
        {
            if (await _equivalanceRequestService.CancelEquivalanceRequest(requestId))
            {
                return Ok(true);
            }
            return BadRequest("Failed to cancel request");
        }
    }
}