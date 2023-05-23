using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northWind.Application.Models
{
    public class EmployeeByCountryModel
    {
        public string Country { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal SaleAmount { get; set; }
    }
}
