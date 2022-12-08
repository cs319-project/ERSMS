using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
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

        [HttpPost()]
        public async Task<ActionResult<CTEFormDto>> AddCTEFormToStudent(CTEFormDto cTEForm)
        {
            //cTEForm.Id = Guid.NewGuid();
            if (await _cTEFormService.AddCTEFormToStudent(cTEForm))
            {
                return Ok(cTEForm);
            }
            return BadRequest("Failed to add CTE Form to Student");
        }

        [HttpPut()]
        public async Task<ActionResult<CTEFormDto>> UpdateCTEForm(CTEFormDto cTEForm)
        {
            if (await _cTEFormService.UpdateCTEForm(cTEForm))
            {
                return Ok(cTEForm);
            }
            return BadRequest("Failed to update CTE Form");
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<CTEFormDto>> GetCTEForms()
        {
            return await _cTEFormService.GetCTEForms();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CTEFormDto>> CancelCTEForm(Guid id)
        {
            var form = _cTEFormService.GetCTEForm(id);

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
            return BadRequest("Failed to get CTE Form");
        }

        [HttpGet("student/{studentID}")]
        public async Task<ActionResult<IEnumerable<CTEFormDto>>> GetCTEFormsOfStudent(string studentID)
        {
            var forms = await _cTEFormService.GetCTEFormsOfStudent(studentID);
            if (forms != null)
            {
                return Ok(forms);
            }
            return BadRequest("Failed to get CTE Forms of Student");
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
    }
}
