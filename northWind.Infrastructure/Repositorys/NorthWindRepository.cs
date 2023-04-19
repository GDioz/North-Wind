using Dapper;
using Microsoft.Extensions.DependencyInjection;
using northWind.Domain.Entites;
using northWind.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
