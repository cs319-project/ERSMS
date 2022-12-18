using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CTEFormController : BaseApiController
    {
        private readonly ICTEFormService _cTEFormService;
        private readonly IUserService _userService;

        public CTEFormController(ICTEFormService cTEFormService, IUserService userService)
        {
            _cTEFormService = cTEFormService;
            _userService = userService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadPdf([FromQuery] Guid formId,
                                                    [FromForm(Name = "pdf")] IFormFile pdf)
        {
            if (pdf == null)
            {
                return BadRequest("No file uploaded");
            }

            var result = await _cTEFormService.UploadPdf(formId, pdf);

            return (result) ? Ok(result) : BadRequest("Error when uploading file");
        }

        [HttpGet("download/{id:guid}")]
        public async Task<ActionResult> DownloadPdf(Guid id)
        {
            var result = await _cTEFormService.DownloadPdf(id);

            // if file extension is .xlsx, return as application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            if (Path.GetExtension(result.Item2) == ".pdf")
            {
                return (result != (null, null)) ? File(result.Item1, "application/pdf", result.Item2) : NotFound();
            }
            else
            {
                return BadRequest("Error when downloading file");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CTEFormDto>> AddCTEFormToStudent(CTEFormDto cTEForm)
        {
            //cTEForm.Id = Guid.NewGuid();
            try
            {
                if (await _cTEFormService.AddCTEFormToStudent(cTEForm))
                {
                    return Ok(cTEForm);
                }
                return BadRequest("Failed to add CTE Form to Student");
            }
            catch (ToDoListException e)
            {
                var result = Accepted();
                result.Value = e.Message;
                return Accepted(result);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<CTEFormDto>> UpdateCTEForm(CTEFormDto cTEForm)
        {
            if (await _cTEFormService.UpdateCTEForm(cTEForm))
            {
                return Ok(cTEForm);
            }
            return BadRequest("Failed to update CTE Form");
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetCTEForms()
        {
            var forms = await _cTEFormService.GetCTEForms();
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CTEFormDto>> CancelCTEForm(Guid id)
        {
            var form = await _cTEFormService.GetCTEForm(id);

            if (form == null)
            {
                return NotFound(id);
            }

            if (await _cTEFormService.DeleteCTEForm(id))
            {
                return Ok(form);
            }
            return BadRequest("Failed to delete CTE Form");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CTEFormDto>> GetCTEForm(Guid id)
        {
            CTEFormDto cTEForm = await _cTEFormService.GetCTEForm(id);
            if (cTEForm != null)
            {
                return Ok(cTEForm);
            }
            return NotFound("Failed to get CTE Form");
        }

        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetCTEFormsOfStudent(string studentID)
        {
            var forms = await _cTEFormService.GetCTEFormsOfStudent(studentID);
            return forms != null ? Ok(forms) : NotFound("Failed to get CTE Forms of Student");
        }

        [HttpGet("archived/all")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetArchivedCTEForms()
        {
            var forms = await _cTEFormService.GetArchivedCTEForms();
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms of Student");
        }

        [HttpGet("archived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetArchivedCTEFormsByDepartment(string userName)
        {
            var forms = await _cTEFormService.GetArchivedCTEFormsByDepartment(userName);
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms of Student");
        }

        [HttpGet("nonarchived/all")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetNonArchivedCTEForms()
        {
            var forms = await _cTEFormService.GetNonArchivedCTEForms();
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms of Student");
        }

        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetNonArchivedCTEFormsByDepartment(string userName)
        {
            var forms = await _cTEFormService.GetNonArchivedCTEFormsByDepartment(userName);
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms of Student");
        }

        [HttpGet("department/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetCTEFormsByDepartment(string userName)
        {
            var forms = await _cTEFormService.GetCTEFormsByDepartment(userName);
            if (forms == null || forms.Count() == 0)
            {
                return NotFound();
            }
            return Ok(forms);
        }

        [HttpPost("deanApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormDean(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormDean(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        [HttpPost("chairApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormChair(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormChair(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        [HttpPost("coordinatorApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormCoordinator(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        [HttpPost("fabApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormFAB(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFacultyOfAdministrationBoard(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        [HttpPatch("cancel/{formId:guid}")]
        public async Task<ActionResult<bool>> CancelCTEFormByStudent(Guid formId)
        {
            if (await _cTEFormService.CancelCTEForm(formId))
            {
                return Ok(true);
            }
            return BadRequest("Failed to cancel CTE Form");
        }
    }
}
