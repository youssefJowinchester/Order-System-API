using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderSystem.Controllers
{

    /// <summary>
    /// Base API controller to be inherited by other API controllers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }

}
