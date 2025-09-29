using Microsoft.AspNetCore.Mvc;
using Service_simple_microservice.Interfaces;

namespace simple_microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? category, [FromQuery] double? minPrice, double? maxPrice)
        {
            try
            {
                var results = await _salesService.GetSalesAsync();

                if (results == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrWhiteSpace(category))
                {
                    results = results.Where(r => r.Category.Equals(category));
                }

                if (minPrice > 0)
                {
                    results = results.Where(r => r.Price >= minPrice);
                }

                if (maxPrice > 0)
                {
                    results = results.Where(r => r.Price <= maxPrice);
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
