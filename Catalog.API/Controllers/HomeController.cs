﻿using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : ControllerBase
{
    [Route("/")]
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}