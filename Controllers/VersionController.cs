using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XinWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        public VersionController() { }

        [HttpGet]
        public async Task<IActionResult> getVersion()
        {
            string versionNumber = "0.0.1";
            string versionDescription = "Added Version Controller";
            string currVersion = $"Version {versionNumber} - Details: {versionDescription}";
            return Ok(currVersion);

        }
    }
}
