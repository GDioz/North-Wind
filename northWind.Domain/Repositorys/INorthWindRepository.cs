using northWind.Domain.Entites;

namespace northWind.Domain.Repositorys
{
    public interface INorthWindRepository
    {
        Task<Order[]> ObterOrders();
        Task<List<Order>> ObterValorTotalPedido();
        Task<List<Order>> ObterOrderPorAno(string year);
        Task<List<Product>> ObterProducts(decimal price);
        Task<List<Product>> ObterProdutosAcimaDaMediaPreco();
        Task<List<Employees>> ObterVendaPorPais();

    }
}
