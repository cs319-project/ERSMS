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
        [HttpPost]
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
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetPreApprovalForms()
        {
            var forms = await _preApprovalFormService.GetPreApprovalForms();
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpPatch("cancel/{id:guid}")]
        public async Task<ActionResult> CancelPreApprovalForm(Guid id)
        {
            var form = await _preApprovalFormService.CancelPreApprovalForm(id);
            return (form) ? Ok() : BadRequest("Failed to cancel Pre-Approval Form");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PreApprovalFormDto>> DeletePreApprovalForm(Guid id)
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
            return NotFound("Failed to get Pre-Approval Form");
        }

        [HttpGet("archived/all")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetArchivedPreApprovalForms()
        {
            var forms = await _preApprovalFormService.GetArchivedPreApprovalForms();
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpGet("archived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetArchivedPreApprovalFormsByDepartment(string userName)
        {
            var forms = await _preApprovalFormService.GetArchivedPreApprovalFormsByDepartment(userName);
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpGet("nonarchived/all")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetNonArchivedPreApprovalForms()
        {
            var forms = await _preApprovalFormService.GetNonArchivedPreApprovalForms();
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetNonArchivedPreApprovalFormsByDepartment(string userName)
        {
            var forms = await _preApprovalFormService.GetNonArchivedPreApprovalFormsByDepartment(userName);
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetPreApprovalFormsOfStudent(string studentId)
        {
            var preApprovalForms = await _preApprovalFormService.GetPreApprovalFormsOfStudent(studentId);
            if (preApprovalForms == null || preApprovalForms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(preApprovalForms);
        }

        [HttpGet("department/{userName}")]
        public async Task<ActionResult<IEnumerable<PreApprovalFormDto>>> GetPreApprovalFormsByDepartment(string userName)
        {
            var preApprovalForms = await _preApprovalFormService.GetPreApprovalFormsByDepartment(userName);
            if (preApprovalForms == null || preApprovalForms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(preApprovalForms);
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
