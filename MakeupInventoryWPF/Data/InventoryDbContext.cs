using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeupInventoryWPF.Models;

namespace MakeupInventoryWPF.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() : base("name=InventoryConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMovement> ProductMovements { get; set; }
    }
}
