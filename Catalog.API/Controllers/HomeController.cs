using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class HomeController : ControllerBase
{
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}