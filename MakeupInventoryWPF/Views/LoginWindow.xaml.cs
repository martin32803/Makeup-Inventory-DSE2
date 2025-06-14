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
using MakeupInventoryWPF.Data;

namespace MakeupInventoryWPF.Views
{
    public partial class LoginWindow : Window
    {
        public int EmployeeID { get; private set; }
        public string EmployeeName { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(EmployeeIdBox.Text, out int empId))
            {
                MessageBox.Show("ID inválido");
                return;
            }

            var context = new InventoryDbContext();
            var user = context.Users.FirstOrDefault(u => u.EmployeeID == empId);

            if (user != null && user.UserPassword == PasswordBox.Password)
            {
                EmployeeID = user.EmployeeID;
                EmployeeName = user.Name;

                CurrentSession.EmployeeID = user.EmployeeID;
                CurrentSession.EmployeeName = user.Name;

                this.DialogResult = true; // esto cierra y devuelve control a App.xaml.cs
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            }
        }
    }
}