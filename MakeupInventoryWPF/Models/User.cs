using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupInventoryWPF.Models
{
    public class User
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string UserPassword { get; set; }
    }
}
