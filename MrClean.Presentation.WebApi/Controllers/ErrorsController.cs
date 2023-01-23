using Microsoft.AspNetCore.Mvc;

namespace MrClean.Presentation.WebApi.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [HttpPost]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
