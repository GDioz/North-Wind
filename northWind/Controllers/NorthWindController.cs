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

        [HttpGet("orders/years")]
        public async Task<IActionResult> ObterOrderPorAno(string year)
        {
            var orders = await _northwindRepository.ObterOrderPorAno(year);
            return Ok(orders);
        }

        [HttpGet("products/{price}")]
        public async Task<IActionResult> ObterProducts(decimal price)
        {
            var products =  await _northwindRepository.ObterProducts(price);
            return Ok(products);
        }

        [HttpGet("products")]
        public async Task<IActionResult> ObterProdutosAcimaDaMediaPreco()
        {
            var products = await _northwindRepository.ObterProdutosAcimaDaMediaPreco();
            return Ok(products);
        }

        [HttpGet("orders/subtotal")]
        public async Task<IActionResult> ObObterValorTotalPedido()
        {
            var orders = await _northwindRepository.ObterValorTotalPedido();
            return Ok(orders);
        }

        [HttpGet("employees/salesamount")]
        public async Task<IActionResult> ObterVendaPorPais()
        {
            var vendas = await _northwindRepository.ObterVendaPorPais();
            return Ok(vendas);
        }
    }

}
