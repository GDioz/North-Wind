using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using northWind.Domain.Repositorys;

namespace northWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NorthWindController : ControllerBase
    {
        private readonly INorthWindRepository _northwindRepository;
        public NorthWindController(INorthWindRepository northWindRepository)
        {
            _northwindRepository = northWindRepository;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> ObterOrders()
        {
            var orders =  await _northwindRepository.ObterOrders();
            return Ok(orders);
        }
    }
}
