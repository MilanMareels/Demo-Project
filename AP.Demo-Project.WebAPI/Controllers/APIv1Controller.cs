using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.Demo_Project.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class APIv1Controller : ControllerBase
    {
    }
}
