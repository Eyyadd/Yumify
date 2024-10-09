using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;

namespace Yumify.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            var response = new GeneralResponse(404, "Not Found");
            return NotFound(response);
        }
    }
}
