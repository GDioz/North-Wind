using northWind.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northWind.Domain.Repositorys
{
    public interface INorthWindRepository
    {
        Task<Order[]> ObterOrders();
    }
}
