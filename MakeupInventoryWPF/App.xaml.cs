using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MakeupInventoryWPF.Views;

namespace MakeupInventoryWPF
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                var main = new MainWindow();
                this.MainWindow = main;
                main.Show();

                // ya no se cierra al cerrar Login
            }
            else
            {
                // se cancela login = cerrar todo
                Shutdown();
            }
        }
    }
}
