using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northWind.Application.Models
{
    public class OrderSubtotalModel
    {
        public int OrderID { get; set; }
        public decimal Subtotal { get; set; }
    }
}
