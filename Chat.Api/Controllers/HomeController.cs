using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

public class HomeController : Controller
{
    [Route("/api/home")]
    public IActionResult Get()
    {
        var value = "testtask";
        return Ok(value);
    }
}