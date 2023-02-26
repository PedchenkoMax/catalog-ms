using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

        return Problem(title: "An error has occurred, try again later or contact support",
            detail: exception.Message);
    }
}