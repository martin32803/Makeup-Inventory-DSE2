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
    public partial class ProductMovementWindow : Window
    {
        public ProductMovementWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(QuantityBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            string sku = SkuBox.Text.Trim();
            string movementType = (MovementTypeBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string reason = ReasonBox.Text.Trim();

            if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(movementType) || string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            var context = new InventoryDbContext();
            var product = context.Products.FirstOrDefault(p => p.SKU == sku);
            if (product == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            int before = product.Stock;
            int after = movementType == "Entrada" ? before + quantity : before - quantity;
            if (after < 0)
            {
                MessageBox.Show("Stock no puede ser negativo.");
                return;
            }

            product.Stock = after;

            var movement = new ProductMovement
            {
                SKU = sku,
                ProductName = product.Name,
                Description = product.Description,
                MovementType = movementType,
                ProductStockBeforeMovement = before,
                ProductStockAfterMovement = after,
                Reason = reason,
                EmployeeID = CurrentSession.EmployeeID,
                EmployeeName = CurrentSession.EmployeeName,
                MovementDate = DateTime.Now
            };

            context.ProductMovements.Add(movement);
            context.SaveChanges();
            MessageBox.Show("Movimiento registrado.");
            this.Close();
        }
    }
}
