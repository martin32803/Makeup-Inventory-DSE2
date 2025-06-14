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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MakeupInventoryWPF.Data;
using MakeupInventoryWPF.Models;
using MakeupInventoryWPF.Views;

namespace MakeupInventoryWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            StartDatePicker.SelectedDate = DateTime.Now.AddDays(-7);
            EndDatePicker.SelectedDate = DateTime.Now;
        }

        private void LoadData()
        {
            using (var context = new InventoryDbContext())
            {
                // Cargar productos
                var productos = context.Products.ToList();
                InventoryGrid.ItemsSource = productos;

                // Cargar movimientos ordenados por fecha descendente
                var movimientos = context.ProductMovements
                    .OrderByDescending(m => m.MovementDate)
                    .ToList();
                MovementsGrid.ItemsSource = movimientos;
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var add = new AddProductWindow();
            add.ShowDialog();
            LoadData();
        }

        private void RegisterMovement_Click(object sender, RoutedEventArgs e)
        {
            var move = new ProductMovementWindow();
            move.ShowDialog();
            LoadData();
        }

        private void ExportReport_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = $"Movimientos_{DateTime.Now:yyyyMMdd_HHmmss}",
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;

                // Usar fechas seleccionadas por el usuario
                if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Selecciona un rango de fechas válido para exportar.", "Fechas requeridas", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var start = StartDatePicker.SelectedDate.Value.Date;
                var end = EndDatePicker.SelectedDate.Value.Date.AddDays(1).AddSeconds(-1); // Fin del día

                try
                {
                    Helpers.ExcelExporter.ExportMovementsToExcel(start, end, filePath);

                    MessageBox.Show("Reporte exportado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    MessageBox.Show("Abriendo el archivo generado...", "Abrir Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al exportar:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana actual (MainWindow)
            this.Hide();

            // Mostrar nuevo login
            var loginWindow = new LoginWindow();
            var result = loginWindow.ShowDialog();

            if (result == true)
            {
                // Login exitoso: mostrar un nuevo MainWindow
                var newMain = new MainWindow();
                Application.Current.MainWindow = newMain;
                newMain.Show();

                // Cerrar esta instancia (que está oculta)
                this.Close();
            }
            else
            {
                // Si canceló el login, cerrar la app
                Application.Current.Shutdown();
            }
        }

        private void InventoryGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var product = e.Row.Item as Product;
            if (product != null && product.Stock < product.MinStock)
                e.Row.Background = new SolidColorBrush(Colors.LightCoral);
        }

        private void FilterMovements_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Selecciona un rango de fechas válido.");
                return;
            }

            var start = StartDatePicker.SelectedDate.Value.Date;
            var end = EndDatePicker.SelectedDate.Value.Date.AddDays(1).AddSeconds(-1); // incluir el final del día

            var context = new InventoryDbContext();
            var movimientosFiltrados = context.ProductMovements
                .Where(m => m.MovementDate >= start && m.MovementDate <= end)
                .OrderByDescending(m => m.MovementDate)
                .ToList();

            MovementsGrid.ItemsSource = movimientosFiltrados;
        }
        private void ClearMovementsFilter_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryDbContext();
            var movimientos = context.ProductMovements
                .OrderByDescending(m => m.MovementDate)
                .ToList();

            MovementsGrid.ItemsSource = movimientos;

            // Opcional: limpia las fechas
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }
    }
}
