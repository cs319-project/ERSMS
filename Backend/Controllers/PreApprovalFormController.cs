using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class PreApprovalFormController : BaseApiController
    {
        private readonly IPreApprovalFormService _preApprovalFormService;
        private readonly IUserService _userService;

        // Constructor
        public PreApprovalFormController(IPreApprovalFormService preApprovalFormService, IUserService userService)
        {
            _preApprovalFormService = preApprovalFormService;
            _userService = userService;
        }

        // Endpoints
        [HttpPost()]
        public async Task<ActionResult<PreApprovalFormDto>> SubmitPreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            try
            {
                if (await _preApprovalFormService.SubmitPreApprovalForm(preApprovalForm))
                {
                    return Ok(preApprovalForm);
                }
                return BadRequest("Failed to add Pre-Approval Form to Student");
            }
            catch (ToDoListException e)
            {
                var result = Accepted();
                result.Value = e.Message;
                return Accepted(result);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<PreApprovalFormDto>> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            if (await _preApprovalFormService.UpdatePreApprovalForm(preApprovalForm))
            {
                return Ok(preApprovalForm);
            }
            return BadRequest("Failed to update Pre-Approval Form");
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<PreApprovalFormDto>> GetPreApprovalForms()
        {
            return await _preApprovalFormService.GetPreApprovalForms();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PreApprovalFormDto>> CancelPreApprovalForm(Guid id)
        {
            var form = await _preApprovalFormService.GetPreApprovalForm(id);

            if (form == null)
            {
                return NotFound(id);
            }

            if (await _preApprovalFormService.DeletePreApprovalForm(id))
            {
                return Ok(form);
            }
            return BadRequest("Failed to delete Pre-Approval Form");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PreApprovalFormDto>> GetPreApprovalForm(Guid id)
        {
            PreApprovalFormDto preApprovalForm = await _preApprovalFormService.GetPreApprovalForm(id);
            if (preApprovalForm != null)
            {
                return Ok(preApprovalForm);
            }
            return BadRequest("Failed to get Pre-Approval Form");
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetPreApprovalFormsOfStudent(string studentId)
        {
            var preApprovalForms = await _preApprovalFormService.GetPreApprovalFormsOfStudent(studentId);
            if (preApprovalForms != null)
            {
                return Ok(preApprovalForms);
            }
            return BadRequest("Failed to get Pre-Approval Form");
        }

        [HttpGet("department/{userName}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetPreApprovalFormsByDepartment(string userName)
        {
            var preApprovalForms = await _preApprovalFormService.GetPreApprovalFormsByDepartment(userName);
            if (preApprovalForms != null)
            {
                return Ok(preApprovalForms);
            }
            return BadRequest("Failed to get Pre-Approval Form");
        }

        [HttpPost("coordinatorApprove/{formId}")]
        public async Task<ActionResult<bool>> CoordinatorApprovePreApprovalForm(Guid formId, ApprovalDto approval)
        {
            if (await _preApprovalFormService.ApproveFormCoordinator(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve Pre-Approval Form");
        }

        [HttpPost("fabApprove/{formId}")]
        public async Task<ActionResult<bool>> FABApprovePreApprovalForm(Guid formId, ApprovalDto approval)
        {
            if (await _preApprovalFormService.ApproveFormFacultyAdministrationBoard(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve Pre-Approval Form");
        }
    }
}
