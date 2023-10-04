using Easyyyyy.Views;
using System.Windows;

namespace Easyyyyy
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
