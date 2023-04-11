using Microsoft.AspNetCore.Mvc;

namespace poc_middleware.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{

    [HttpGet(Name = "Get")]
    public IEnumerable<string> Get()
    {
        return new List<string>();
    }
}

