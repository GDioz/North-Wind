using Dapper;
using Microsoft.Extensions.DependencyInjection;
using northWind.Domain.Entites;
using northWind.Domain.Repositorys;
using northWind.Infrastructure.Scripts;
using System.Data;
using System.Data.SqlClient;

namespace northWind.Infrastructure.Repositorys
{
    public class NorthWindRepository : INorthWindRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public NorthWindRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Order[]> ObterOrders()
        {
            var lista = await Query<Order>(NorthWindScripts.SQL_OBTER_ORDERS);
            return lista.ToArray();
        }

        public async Task<List<Product>> ObterProducts()
        {
            var lista = await Query<Product>(NorthWindScripts.SQL_OBTER_PRODUTOS);
            return lista;
        }
        public async Task<List<Employees>> ObterEmployees()
        {
            var lista = await Query<Employees>(NorthWindScripts.SQL_OBTER_FUNCIONARIOS);
            return lista;
        }

        public async Task<List<OrderDetails>> ObterOrderDetails()
        {
            var lista = await Query<OrderDetails>(NorthWindScripts.SQL_OBTER_ORDER_DETAILS);
            return lista;
        }

        private async Task<List<T>> Query<T>(string sql, DynamicParameters? parametros = null)
        {
            try
            {
                return (await _serviceProvider.GetService<SqlConnection>().QueryAsync<T>(sql, parametros)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
                throw;
            }
        }
    }
}
