using System.Linq;
using System.Windows;

namespace EMS.Views
{
    public partial class LogoutWindow : Window
    {
        public LogoutWindow()
        {
            InitializeComponent();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            // Prevent app shutdown while closing windows
            var previousMode = Application.Current.ShutdownMode;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            WindowManager.CloseAll();

            var login = new LoginWindow();
            login.Show();

            Application.Current.ShutdownMode = previousMode;

            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
