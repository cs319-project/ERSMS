using Backend.DTOs;
using Backend.Entities.Exceptions;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the CTE (Course Transfer and Exemption) Forms API</summary>
    public class CTEFormController : BaseApiController
    {
        private readonly ICTEFormService _cTEFormService;
        private readonly IUserService _userService;

        /// <summary>Initializes a new instance of the <see cref="CTEFormController"/> class.</summary>
        /// <param name="cTEFormService">The CTEForm service.</param>
        /// <param name="userService">The user service.</param>
        public CTEFormController(ICTEFormService cTEFormService, IUserService userService)
        {
            _cTEFormService = cTEFormService;
            _userService = userService;
        }

        /// <summary>Uploads a PDF file to the server.</summary>
        /// <param name="formId">The ID of the form to which the file belongs.</param>
        /// <param name="pdf">The PDF file to upload.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the success of the operation.</returns>
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

        /// <summary>Downloads a PDF file.</summary>
        /// <param name="id">The ID of the CTEForm.</param>
        /// <returns>The PDF file.</returns>
        /// <exception cref="ArgumentException">Thrown when the ID is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the application isn't in foreground.</exception>
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

        /// <summary>Adds a CTE Form to a Student.</summary>
        /// <param name="cTEForm">The CTE Form to add.</param>
        /// <returns>The CTE Form that was added.</returns>
        /// <exception cref="ToDoListException">Thrown when the CTE Form could not be added.</exception>
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

        /// <summary>Updates a CTE Form.</summary>
        /// <param name="cTEForm">The CTE Form to update.</param>
        /// <returns>The updated CTE Form.</returns>
        [HttpPut("update")]
        public async Task<ActionResult<CTEFormDto>> UpdateCTEForm(CTEFormDto cTEForm)
        {
            if (await _cTEFormService.UpdateCTEForm(cTEForm))
            {
                return Ok(cTEForm);
            }
            return BadRequest("Failed to update CTE Form");
        }

        /// <summary>Gets all CTE forms.</summary>
        /// <returns>All CTE forms.</returns>
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

        /// <summary>Cancels a CTE Form.</summary>
        /// <param name="id">The ID of the CTE Form to cancel.</param>
        /// <returns>The canceled CTE Form.</returns>
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

        /// <summary>Gets a CTE Form.</summary>
        /// <param name="id">The ID of the CTE Form.</param>
        /// <returns>The CTE Form.</returns>
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

        /// <summary>Gets the CTE Forms of a Student.</summary>
        /// <param name="studentID">The ID of the Student.</param>
        /// <returns>The CTE Forms of the Student.</returns>
        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetCTEFormsOfStudent(string studentID)
        {
            var forms = await _cTEFormService.GetCTEFormsOfStudent(studentID);
            return forms != null ? Ok(forms) : NotFound("Failed to get CTE Forms of Student");
        }

        /// <summary>Gets all archived CTE Forms.</summary>
        /// <returns>All archived CTE Forms.</returns>
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

        /// <summary>Gets all archived CTE Forms of a Student.</summary>
        /// <param name="userName">The username of the Student.</param>
        /// <returns>The archived CTE Forms of the Student.</returns>
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

        /// <summary>Gets all non-archived CTE Forms.</summary>
        /// <returns>All non-archived CTE Forms.</returns>
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

        /// <summary>Gets all non-archived CTE Forms of a Student.</summary>
        /// <param name="userName">The username of the Student.</param>
        /// <returns>A list of non-archived CTE Forms of the Student.</returns>
        [HttpGet("nonarchived/department/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetNonArchivedCTEFormsByDepartment(string userName)
        {
            var forms = await _cTEFormService.GetNonArchivedCTEFormsByDepartment(userName);
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms by Department");
        }

        /// <summary>Gets all non-archived CTE Forms for a Chair.</summary>
        /// <param name="userName">The username of the Chair.</param>
        /// <returns>A list of non-archived CTE Forms for a Chair.</returns>
        [HttpGet("nonarchived/department/chair/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetNonArchivedCTEFormsByDepartmentForChair(string userName)
        {
            var forms = await _cTEFormService.GetNonArchivedCTEFormsByDepartmentForChair(userName);
            if (forms != null && forms.Count() != 0)
            {
                return Ok(forms);
            }
            else if (forms.Count() == 0)
            {
                return Ok("No non-archived CTE Forms");
            }
            else
            {
                return BadRequest("Failed to get CTE Forms by Department");
            }
        }

        /// <summary>Gets all archived CTE Forms for a Chair.</summary>
        /// <param name="userName">The username of the Chair.</param>
        /// <returns>A list of archived CTE Forms for a Chair.</returns>
        [HttpGet("archived/department/chair/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetArchivedCTEFormsByDepartmentForChair(string userName)
        {
            var forms = await _cTEFormService.GetArchivedCTEFormsByDepartmentForChair(userName);
            if (forms != null && forms.Count() != 0)
            {
                return Ok(forms);
            }
            else if (forms.Count() == 0)
            {
                return Ok("No archived CTE Forms");
            }
            else
            {
                return BadRequest("Failed to get CTE Forms by Department");
            }
        }

        /// <summary>Gets all non-archived CTE Forms by a Faculty.</summary>
        /// <param name="userName">The username of the Dean.</param>
        /// <returns>A list of non-archived CTE Forms of the Faculty.</returns>
        [HttpGet("nonarchived/faculty/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetNonArchivedCTEFormsByFacultyForDean(string userName)
        {
            var forms = await _cTEFormService.GetNonArchivedCTEFormsByFacultyForDean(userName);
            if (forms != null && forms.Count() != 0)
            {
                return Ok(forms);
            }
            else if (forms.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest("Failed to get CTE Forms by Faculty");
            }
        }

        /// <summary>Gets all rchived CTE Forms by a Faculty.</summary>
        /// <param name="userName">The username of the Dean.</param>
        /// <returns>A list of archived CTE Forms of the Faculty.</returns>
        [HttpGet("archived/faculty/{userName}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetArchivedCTEFormsByFacultyForDean(string userName)
        {
            var forms = await _cTEFormService.GetArchivedCTEFormsByFacultyForDean(userName);
            if (forms != null && forms.Count() != 0)
            {
                return Ok(forms);
            }
            else if (forms.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest("Failed to get CTE Forms by Faculty");
            }
        }

        /// <summary>Gets all CTE forms for a given department.</summary>
        /// <param name="userName">The username of the department.</param>
        /// <returns>All CTE forms for the department.</returns>
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

        /// <summary>Approve a CTE Form by Dean.</summary>
        /// <param name="formId">The ID of the CTE Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="bool"/> indicating whether the operation succeeded.</returns>
        [HttpPost("deanApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormDean(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormDean(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        /// <summary>Approve a CTE Form by the Chair.</summary>
        /// <param name="formId">The ID of the CTE Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="bool"/> indicating whether the operation was successful.</returns>
        [HttpPost("chairApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormChair(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormChair(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        /// <summary>Approve a CTE Form by the Coordinator.</summary>
        /// <param name="formId">The ID of the CTE Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="bool"/> indicating whether the CTE Form was approved.</returns>
        [HttpPost("coordinatorApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFormCoordinator(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        /// <summary>Approve a CTE Form.</summary>
        /// <param name="formId">The ID of the CTE Form.</param>
        /// <param name="approval">The approval data.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("fabApprove/{formId}")]
        public async Task<ActionResult<bool>> ApproveFormFAB(Guid formId, ApprovalDto approval)
        {
            if (await _cTEFormService.ApproveFacultyOfAdministrationBoard(formId, approval))
            {
                return Ok(true);
            }
            return BadRequest("Failed to approve CTE Form");
        }

        /// <summary>Cancels a CTE Form.</summary>
        /// <param name="formId">The ID of the CTE Form to cancel.</param>
        /// <returns>A <see cref="bool"/> indicating whether the CTE Form was successfully cancelled.</returns>
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
