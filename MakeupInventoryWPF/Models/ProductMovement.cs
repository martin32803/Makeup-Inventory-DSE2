using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupInventoryWPF.Models
{
    public class ProductMovement
    {
        [Key]
        public int MovementID { get; set; }

        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string MovementType { get; set; }
        public int ProductStockBeforeMovement { get; set; }
        public int ProductStockAfterMovement { get; set; }
        public string Reason { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
