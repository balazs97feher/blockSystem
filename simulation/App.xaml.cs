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
            Control Controller = Control.CreateController();
            SimulationWindow AppWindow = SimulationWindow.CreateWindow(Controller);
            Layout.Initialize(AppWindow.AppCanvas);
            Controller.Initialize();

            Layout.Draw();
            AppWindow.Title = "Automatikus térköz szimulációja";
            AppWindow.Show();
        }

    }
}
