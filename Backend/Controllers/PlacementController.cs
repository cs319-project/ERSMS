using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Net.Http.Headers;

namespace Backend.Controllers
{
    public class PlacementController : BaseApiController
    {
        private readonly IPlacementService _placementService;

        public PlacementController(IPlacementService placementService, IMapper mapper)
        {
            _placementService = placementService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadPlacementTable([FromQuery] String facultyName, [FromQuery] String departmentName, [FromForm(Name = "placementTable")] IFormFile placementTable)
        {
            if (placementTable == null)
            {
                return BadRequest("No file uploaded");
            }

            var result = await _placementService.UploadPlacementTable(facultyName, departmentName, placementTable);

            return (result != null) ? Ok(result) : BadRequest("Error when uploading file");
        }

        [HttpGet("download/{id:guid}")]
        public async Task<ActionResult> DownloadPlacementTable(Guid id)
        {
            var result = await _placementService.DownloadPlacementTable(id);

            // if file extension is .xlsx, return as application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            if (Path.GetExtension(result.Item2) == ".xlsx")
            {
                return (result != (null, null)) ? File(result.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.Item2) : NotFound();
            }
            // if file extension is .xls, return as application/vnd.ms-excel
            else if (Path.GetExtension(result.Item2) == ".xls")
            {
                return (result != (null, null)) ? File(result.Item1, "application/vnd.ms-excel", result.Item2) : NotFound();
            }
            else
            {
                return BadRequest("Error when downloading file");
            }
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult> DeletePlacementTable(Guid id)
        {
            return (await _placementService.DeletePlacementTable(id)) ? Ok() : NotFound();
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<PlacementTableDto>>> GetAllPlacementTables()
        {
            var result = await _placementService.GetAllPlacementTables();

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<PlacementTableDto>>> GetPlacementTables([FromQuery] String facultyName, [FromQuery] String departmentName)
        {
            var result = await _placementService.GetPlacementTablesByDepartment(facultyName, departmentName);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("get/{id:guid}")]
        public async Task<ActionResult<PlacementTableDto>> GetPlacementTable(Guid id)
        {
            var result = await _placementService.GetPlacementTable(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        [HttpGet("placeStudents/{id:guid}")]
        public async Task<ActionResult<IEnumerable<PlacedStudentDto>>> PlaceStudents(Guid id)
        {
            var result = await _placementService.PlaceStudents(id);

            return (result != null) ? Ok(result) : BadRequest("Error when placing students");
        }
    }
}
