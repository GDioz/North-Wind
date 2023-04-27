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

        public async Task<List<Order>> ObterOrderPorAno(string year)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@year", year, DbType.String);

            List<Order> lista = await Query<Order>(NorthWindScripts.SQL_OBTER_PRODUTO_POR_ANO, parametros);
            return lista;
        }

        public async Task<Order[]> ObterOrders()
        {
            var lista = await Query<Order>(NorthWindScripts.SQL_OBTER_ORDERS);
            return lista.ToArray();
        }

        public async Task<List<Product>> ObterProducts(decimal price)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@price", price, DbType.Decimal);

            var lista = await Query<Product>(NorthWindScripts.SQL_OBTER_PRODUTOS, parametros);
            return lista;
        }

        public async Task<List<Product>> ObterProdutosAcimaDaMediaPreco()
        {
            var lista = await Query<Product>(NorthWindScripts.SQL_OBTER_PRODUTO_ACIMA_DA_MEDIA_PRECO);
            return lista;
        }

        public async Task<List<Order>> ObterValorTotalPedido()
        {
            var lista = await Query<Order>(NorthWindScripts.SQL_OBTER_VALOR_TOTAL_PEDIDO);
            return lista;
        }

        public async Task<List<Employees>> ObterVendaPorPais()
        {
            var lista = await Query<Employees>(NorthWindScripts.SQL_OBTER_VENDA_POR_PAIS);
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
