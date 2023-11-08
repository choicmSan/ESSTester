using ESSTester.UI.Local.ViewModels;
using ESSTester.UI.UI.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ESSTester
{
    internal class App
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Window mainWindow = null;
            mainWindow = new ESSTesterWindow();
            //mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //mainWindow.Topmost = true;
            mainWindow.DataContext = new ESSTesterWindowViewModel();

            Application app = new Application();
            app.Run(mainWindow);
        }
    }
}
