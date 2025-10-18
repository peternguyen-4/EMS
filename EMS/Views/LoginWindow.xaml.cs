using EMS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EMS.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserRepository userRepo = new UserRepository();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            bool isValid = userRepo.ValidateUser(username,password);

            if (isValid)
            {
                MessageBox.Show("Login successful!");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
