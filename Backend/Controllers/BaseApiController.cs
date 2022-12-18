using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Base class for API controllers.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}
