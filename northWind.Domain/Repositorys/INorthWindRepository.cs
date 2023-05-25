using northWind.Domain.Entites;

namespace northWind.Domain.Repositorys
{
    public interface INorthWindRepository
    {
        Task<Order[]> ObterOrders();
        Task<List<OrderDetails>> ObterOrderDetails();
        Task<List<Product>> ObterProducts();
        Task<List<Employees>> ObterEmployees();
    }
}
