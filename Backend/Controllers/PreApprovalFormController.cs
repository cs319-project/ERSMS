using Backend.DTOs;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the Pre-Approval Form API.</summary>
    public class PreApprovalFormController : BaseApiController
    {
        private readonly IPreApprovalFormService _preApprovalFormService;
        private readonly IUserService _userService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="PreApprovalFormController"/> class.</summary>
        /// <param name="preApprovalFormService">The pre approval form service.</param>
        /// <param name="userService">The user service.</param>
        public PreApprovalFormController(IPreApprovalFormService preApprovalFormService, IUserService userService)
        {
            _preApprovalFormService = preApprovalFormService;
            _userService = userService;
        }

        // Endpoints

        /// <summary>Uploads a PDF file to the pre-approval form.</summary>
        /// <param name="formId">The ID of the pre-approval form.</param>
        /// <param name="pdf">The PDF file to upload.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the upload was successful.</returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadPdf([FromQuery] Guid formId,
                                                    [FromForm(Name = "pdf")] IFormFile pdf)
        {
            if (pdf == null)
            {
                return BadRequest("No file uploaded");
            }

            var result = await _preApprovalFormService.UploadPdf(formId, pdf);

            return (result) ? Ok(result) : BadRequest("Error when uploading file");
        }

        /// <summary>Downloads a PDF file.</summary>
        /// <param name="id">The ID of the pre-approval form.</param>
        /// <returns>The PDF file.</returns>
        [HttpGet("download/{id:guid}")]
        public async Task<ActionResult> DownloadPdf(Guid id)
        {
            var result = await _preApprovalFormService.DownloadPdf(id);

            if (Path.GetExtension(result.Item2) == ".pdf")
            {
                return (result != (null, null)) ? File(result.Item1, "application/pdf", result.Item2) : NotFound();
            }
            else
            {
                return BadRequest("Error when downloading file");
            }
        }

        /// <summary>Submits a Pre-Approval Form to the Student.</summary>
        /// <param name="preApprovalForm">The Pre-Approval Form to submit.</param>
        /// <returns>The submitted Pre-Approval Form.</returns>
        /// <exception cref="ToDoListException">Thrown when the Pre-Approval Form fails to submit.</exception>
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

        /// <summary>Updates a Pre-Approval Form.</summary>
        /// <param name="preApprovalForm">The Pre-Approval Form to update.</param>
        /// <returns>The updated Pre-Approval Form.</returns>
        [HttpPut()]
        public async Task<ActionResult<PreApprovalFormDto>> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            if (await _preApprovalFormService.UpdatePreApprovalForm(preApprovalForm))
            {
                return Ok(preApprovalForm);
            }
            return BadRequest("Failed to update Pre-Approval Form");
        }

        /// <summary>Gets all pre-approval forms.</summary>
        /// <returns>All pre-approval forms.</returns>
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

        /// <summary>Cancels a Pre-Approval Form.</summary>
        /// <param name="id">The ID of the Pre-Approval Form to cancel.</param>
        /// <returns>An ActionResult indicating whether the Pre-Approval Form was successfully cancelled.</returns>
        [HttpPatch("cancel/{id:guid}")]
        public async Task<ActionResult> CancelPreApprovalForm(Guid id)
        {
            var form = await _preApprovalFormService.CancelPreApprovalForm(id);
            return (form) ? Ok() : BadRequest("Failed to cancel Pre-Approval Form");
        }

        /// <summary>Deletes a Pre-Approval Form.</summary>
        /// <param name="id">The ID of the Pre-Approval Form to delete.</param>
        /// <returns>The deleted Pre-Approval Form.</returns>
        /// <exception cref="NotFoundException">Thrown when the Pre-Approval Form is not found.</exception>
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

        /// <summary>Gets a Pre-Approval Form.</summary>
        /// <param name="id">The Pre-Approval Form ID.</param>
        /// <returns>The Pre-Approval Form.</returns>
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

        /// <summary>Gets all archived pre-approval forms.</summary>
        /// <returns>All archived pre-approval forms.</returns>
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

        /// <summary>Retrieves the archived pre-approval forms by department.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The archived pre-approval forms by department.</returns>
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

        /// <summary>Gets all non-archived pre-approval forms.</summary>
        /// <returns>All non-archived pre-approval forms.</returns>
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

        /// <summary>Gets all non-archived pre-approval forms for a department.</summary>
        /// <param name="userName">The user name of the department.</param>
        /// <returns>The non-archived pre-approval forms for the department.</returns>
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

        /// <summary>Gets the pre-approval forms of a student.</summary>
        /// <param name="studentId">The student's ID.</param>
        /// <returns>The pre-approval forms of the student.</returns>
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

        /// <summary>Gets the pre-approval forms by department.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The pre-approval forms by department.</returns>
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

        /// <summary>Approves a Pre-Approval Form by the Coordinator.</summary>
        /// <param name="formId">The ID of the Pre-Approval Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="bool"/> indicating whether the operation was successful.</returns>
        [HttpPost("coordinatorApprove/{formId}")]
        public async Task<ActionResult<bool>> CoordinatorApprovePreApprovalForm(Guid formId, ApprovalDto approval)
        {
            if (await _preApprovalFormService.ApproveFormCoordinator(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve Pre-Approval Form");
        }

        /// <summary>Approves a Pre-Approval Form.</summary>
        /// <param name="formId">The ID of the Pre-Approval Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="bool"/> indicating whether the operation succeeded.</returns>
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
