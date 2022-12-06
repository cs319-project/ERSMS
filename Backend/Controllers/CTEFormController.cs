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

        public CTEFormController(ICTEFormService cTEFormService)
        {
            _cTEFormService = cTEFormService;
        }

        [HttpPost()]
        public async Task<ActionResult<CTEFormDto>> AddCTEFormToStudent(CTEFormDto cTEForm)
        {
            if (await _cTEFormService.AddCTEFormToStudent(cTEForm.SubjectStudentUserName, cTEForm))
            {
                return Ok(cTEForm);
            }
            return BadRequest("Failed to add CTE Form to Student");
        }
    }
}
