using EMS.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }

}
