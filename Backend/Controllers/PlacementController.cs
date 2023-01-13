using AutoMapper;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the Student Placement API.</summary>
    public class PlacementController : BaseApiController
    {
        private readonly IPlacementService _placementService;

        /// <summary>Initializes a new instance of the <see cref="PlacementController"/> class.</summary>
        /// <param name="placementService">The placement service.</param>
        /// <param name="mapper">The mapper.</param>
        public PlacementController(IPlacementService placementService, IMapper mapper)
        {
            _placementService = placementService;
        }

        /// <summary>Uploads a placement table.</summary>
        /// <param name="facultyName">The name of the faculty.</param>
        /// <param name="departmentName">The name of the department.</param>
        /// <param name="placementTable">The placement table.</param>
        /// <returns>The result of the upload.</returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        // [Authorize(Roles = "Office of International Students and Exchange Programs")]
        public async Task<ActionResult> UploadPlacementTable([FromQuery] String facultyName,
                                                                [FromQuery] String departmentName,
                                                                [FromForm(Name = "placementTable")] IFormFile placementTable)
        {
            try
            {
                if (placementTable == null)
                {
                    return BadRequest("No file uploaded");
                }

                var result = await _placementService.UploadPlacementTable(facultyName, departmentName, placementTable);

                return (result != null) ? Ok(result) : BadRequest("Error when uploading file");
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>Downloads the placement table.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placement table as a file.</returns>
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

        /// <summary>Deletes a placement table.</summary>
        /// <param name="id">The ID of the placement table to delete.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the placement table was deleted.</returns>
        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult> DeletePlacementTable(Guid id)
        {
            return (await _placementService.DeletePlacementTable(id)) ? Ok() : NotFound();
        }

        /// <summary>Gets all placement tables.</summary>
        /// <returns>All placement tables.</returns>
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<PlacementTableDto>>> GetAllPlacementTables()
        {
            var result = await _placementService.GetAllPlacementTables();

            return Ok(result);
        }

        /// <summary>Gets the placement tables for a given department.</summary>
        /// <param name="facultyName">The name of the faculty.</param>
        /// <param name="departmentName">The name of the department.</param>
        /// <returns>The placement tables for the given department.</returns>
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<PlacementTableDto>>> GetPlacementTables([FromQuery] String facultyName, [FromQuery] String departmentName)
        {
            var result = await _placementService.GetPlacementTablesByDepartment(facultyName, departmentName);

            return (result != null) ? Ok(result) : NotFound();
        }

        /// <summary>Gets a placement table.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placement table.</returns>
        [HttpGet("get/{id:guid}")]
        public async Task<ActionResult<PlacementTableDto>> GetPlacementTable(Guid id)
        {
            var result = await _placementService.GetPlacementTable(id);

            return (result != null) ? Ok(result) : NotFound();
        }

        /// <summary>Places students in the specified class.</summary>
        /// <param name="id">The ID of the class to place students in.</param>
        /// <returns>A list of placed students.</returns>
        [HttpPost("placeStudents/{id:guid}")]
        public async Task<ActionResult<IEnumerable<PlacedStudentDto>>> PlaceStudents(Guid id)
        {
            var result = await _placementService.PlaceStudents(id);

            return (result != null) ? Ok(result) : BadRequest("Error when placing students");
        }
    }
}
