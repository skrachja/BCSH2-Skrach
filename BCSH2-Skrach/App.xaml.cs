using System;
using System.Windows;

namespace BCSH2_Skrach
{
    public partial class App : Application
    {
        private MainWindow mainWindow;
        private LoginView loginWindow = new LoginView();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
        public void OnSuccessfulLogin()
        {
        }
    }
}
