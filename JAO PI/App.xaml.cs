
using System.Windows;

namespace JAO_PI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Views.Main window = new Views.Main();
            Application.Current.MainWindow = window;

            window.Show();
        }
    }
}
