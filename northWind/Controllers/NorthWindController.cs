using Microsoft.AspNetCore.Mvc;
using northWind.Application.Models;
using northWind.Domain.Entites;
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
            var orders = await _northwindRepository.ObterOrders();
            return Ok(orders);
        }

        [HttpGet("orders/years")]
        public async Task<IActionResult> ObterOrderPorAno(int year)
        {
            var orders = from order in await _northwindRepository.ObterOrders()
                         join detail in await _northwindRepository.ObterOrderDetails()
                            on order.OrderID equals detail.OrderID
                         where order.ShippedDate.HasValue && order.ShippedDate.Value.Year == year
                         orderby order.OrderID descending
                         select new OrderByYearModel()
                         {
                             OrderID = order.OrderID,
                             ShippedDate = order.ShippedDate,
                             Subtotal = detail.UnitPrice * detail.Quantity,
                             Year = order.ShippedDate.HasValue ? order.ShippedDate.Value.Year : 0
                         };
            return Ok(orders);
        }

        [HttpGet("products/{price}")]
        public async Task<IActionResult> ObterProducts(decimal price)
        {
            var products = from product in await _northwindRepository.ObterProducts()
                           where product.UnitPrice < price
                           orderby product.UnitPrice descending
                           select new ProductPriceModel()
                           {
                               ProductID = product.ProductID,
                               ProductName = product.ProductName,
                               UnitPrice = product.UnitPrice,
                           };
            return Ok(products);
        }

        [HttpGet("products")]
        public async Task<IActionResult> ObterProdutosAcimaDaMediaPreco()
        {
            var products = await _northwindRepository.ObterProducts();
            decimal averagePrice = products.Average(x => x.UnitPrice);
            var result = from product in products
                           where product.UnitPrice > averagePrice
                           orderby product.UnitPrice ascending
                           select new ProductsAbovePriceModel()
                           {
                               ProductName = product.ProductName,
                               UnitPrice = product.UnitPrice
                           };
            return Ok(result);
        }

        [HttpGet("orders/subtotal")]
        public async Task<IActionResult> ObObterValorTotalPedido()
        {
            var orders = from order in await _northwindRepository.ObterOrders()
                         join detail in await _northwindRepository.ObterOrderDetails()
                            on order.OrderID equals detail.OrderID
                         group  new {order, detail} by order.OrderID into g
                         select new OrderSubtotalModel
                         {
                             OrderID = g.Key,
                             Subtotal = g.Sum(x => x.detail.UnitPrice * x.detail.Quantity),
                         };
            return Ok(orders);
        }

        [HttpGet("employees/salesamount")]
        public async Task<IActionResult> ObterVendaPorPais()
        {
            var employees = await _northwindRepository.ObterEmployees();
            var orders = await _northwindRepository.ObterOrders();
            var orderDetails = await _northwindRepository.ObterOrderDetails();

            var vendas = from employee in employees
                         join order in orders
                            on employee.EmployeeID equals order.EmployeeID
                         join detail in orderDetails
                            on order.OrderID equals detail.OrderID
                        group new {employee, order, detail} by new { 
                                                                     employee.Country, 
                                                                     employee.LastName, 
                                                                     employee.FirstName, 
                                                                     order.OrderID, 
                                                                     order.ShippedDate
                                                                   } into g
                         select new EmployeeByCountryModel
                         {
                             Country = g.Key.Country,
                             LastName = g.Key.LastName,
                             FirstName = g.Key.FirstName,
                             ShippedDate = g.Key.ShippedDate,
                             OrderId = g.Key.OrderID,
                             SaleAmount = g.Sum(x => x.detail.UnitPrice * x.detail.Quantity)
                         };

            return Ok(vendas);
        }
    }

}
