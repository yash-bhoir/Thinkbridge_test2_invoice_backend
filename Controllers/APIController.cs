using Microsoft.AspNetCore.Mvc;

namespace test_2_debugging_code.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            string result = "Data fetched";
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new { message = result });
            }
            return BadRequest("No data");
        }
    }
}
