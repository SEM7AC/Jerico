using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEM7AC.U.SystemInfo;

namespace Jerico.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartupController : ControllerBase
    {
    [HttpGet]
    public IActionResult Get()
        {
        #pragma warning disable CA1416   // Validate platform compatibility
        var info = SysInfo.Get();       // Windows Only Package 
        #pragma warning restore CA1416 // Validate platform compatibility

        return Ok(new
            {
            Status = "Online",
            Boot = DateTime.UtcNow,
            System = info,
            Message = "Jerico API startup diagnostics"
            });
        }
    }

