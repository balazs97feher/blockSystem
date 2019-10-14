using System.Windows;

namespace simulation
{
    public partial class App : Application
    {
        private Communication Messenger;
        private Control Controller;
        private SimulationWindow AppWindow;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Messenger = Communication.CreateMessenger();
            Controller = Control.CreateController(Messenger);
            AppWindow = SimulationWindow.CreateWindow(Controller, Messenger);
            Layout.Initialize(AppWindow.AppCanvas, Messenger);
            Controller.Initialize();

            Layout.Draw();
            AppWindow.Title = "Automatikus térköz szimulációja";
            AppWindow.Show();
        }


        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Messenger.BoosterDisconnect();
            Messenger.ClosePorts();
        }
    }
}
