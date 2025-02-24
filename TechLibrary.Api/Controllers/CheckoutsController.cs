using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckoutsController : ControllerBase
{
    [HttpPost]
    public IActionResult BookCheckout(Guid boodId)
    {
        return NoContent();
    }
}
