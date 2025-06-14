using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MakeupInventoryWPF.Models;
using MakeupInventoryWPF.Data;

namespace MakeupInventoryWPF.Views
{
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SkuBox.Text.Length != 6 || !int.TryParse(SkuBox.Text, out _))
            {
                MessageBox.Show("El SKU debe ser un número de 6 dígitos.");
                return;
            }

            if (!int.TryParse(StockBox.Text, out int stock) ||
                !int.TryParse(MinStockBox.Text, out int minStock) ||
                !int.TryParse(MaxStockBox.Text, out int maxStock))
            {
                MessageBox.Show("Los campos de stock deben ser números.");
                return;
            }

            var product = new Product
            {
                Name = NameBox.Text,
                SKU = SkuBox.Text,
                Description = DescriptionBox.Text,
                Stock = stock,
                MinStock = minStock,
                MaxStock = maxStock
            };

            var context = new InventoryDbContext();
            context.Products.Add(product);
            context.SaveChanges();

            MessageBox.Show("Producto agregado exitosamente.");
            this.Close();
        }
    }
}
