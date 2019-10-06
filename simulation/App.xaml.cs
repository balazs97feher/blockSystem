using System.Windows;

namespace simulation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Control Controller = new Control();
            MainWindow AppWindow = new MainWindow(Controller);
            Layout.Initialize(AppWindow.AppCanvas);
            Controller.Initialize();

            Layout.Draw();
            AppWindow.Title = "Automatikus térköz szimulációja";
            AppWindow.Show();
        }

    }
}
