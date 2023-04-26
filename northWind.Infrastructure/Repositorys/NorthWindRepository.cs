using Dapper;
using Microsoft.Extensions.DependencyInjection;
using northWind.Domain.Entites;
using northWind.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northWind.Infrastructure.Repositorys
{
    public class NorthWindRepository : INorthWindRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public NorthWindRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider= serviceProvider;
        }

        public async Task<List<Order>> ObterOrderPorAno(string year)
        {
            const string sql = "SELECT " +
                   "YEAR(ShippedDate) AS Year, " +
                   "O.OrderID AS OrderId, " +
                   "SUM(UnitPrice * Quantity) AS Subtotal, " +
                   "ShippedDate " +
                   "FROM dbo.Orders O INNER JOIN dbo.[Order Details] D ON O.OrderID = D.OrderID " +
                   "WHERE YEAR(@year) = YEAR(ShippedDate) " +
                   "AND YEAR(@year) BETWEEN YEAR('1996-12-24') AND YEAR('1997-09-30') " +
                   "GROUP BY " +
                   "YEAR(ShippedDate), " +
                   "O.OrderID, " +
                   "ShippedDate ";

            var parametros = new DynamicParameters();
            parametros.Add("@year", year, DbType.String);

            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Order>(sql,parametros)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }
        }

        public async Task<Order[]> ObterOrders()
        {
            const string sql = "SELECT TOP 500 " +
                "OrderID as OrderID, " +
                "CustomerID as CustomerID, " +
                "OrderDate as OrderDate, " +
                "ShippedDate as ShippedDate, " +
                "ShipName as ShipName" +
                " FROM dbo.Orders";

            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Order>(sql)).ToArray();
                return lista;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }

        }

        public async Task<List<Product>> ObterProducts(decimal price)
        {
            const string sql = "SELECT TOP 500 " +
                               "ProductID as ProductID, " +
                               "ProductName as ProductName, " +
                               "UnitPrice as UnitPrice " +
                               " FROM dbo.Products " +
                               "WHERE UnitPrice <  @price";
            var parametros = new DynamicParameters();
                parametros.Add("@price", price, DbType.Decimal);

            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Product>(sql , parametros)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }

        }

        public async Task<List<Product>> ObterProdutosAcimaDaMediaPreco()
        {
            const string sql = "SELECT TOP 500 " +
                               "ProductID as ProductID, " +
                               "ProductName as ProductName, " +
                               "UnitPrice as UnitPrice " +
                               " FROM dbo.Products " +
                               "WHERE UnitPrice > (SELECT AVG(UnitPrice) From dbo.Products) " +
                               "ORDER BY UnitPrice ASC";
            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Product>(sql)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }
        }

        public async Task<List<Order>> ObterValorTotalPedido()
        {
            const string sql = "SELECT OrderID as OrderID, " +
                               "SUM(UnitPrice * Quantity) as Subtotal " +
                               "FROM dbo.[Order Details] " +
                               "GROUP BY OrderID";

            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Order>(sql)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }
        }

        public async Task<List<Employees>> ObterVendaPorPais()
        {
            const string sql = "SELECT E.Country,E.LastName,E.FirstName, " +
                               "O.ShippedDate,O.OrderID, " +
                               "ROUND(SUM(D.Quantity*D.UnitPrice),2) AS SaleAmount " +
                               "FROM dbo.Employees E " +
                               "INNER JOIN dbo.Orders O " +
                               "ON E.EmployeeID = O.EmployeeID " +
                               "INNER JOIN dbo.[Order Details] D " +
                               "ON O.OrderID = D.OrderID " +
                               "GROUP BY E.Country,E.LastName,E.FirstName,O.OrderID,O.ShippedDate ";

            try
            {
                var lista = (await _serviceProvider.GetService<SqlConnection>().QueryAsync<Employees>(sql)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }
        }
    }
}
