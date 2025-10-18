using EMS.Core.Models;
using EMS.Views;
using System.Windows;

namespace EMS.Views
{
    public partial class UserDetailsWindow : Window
    {
        private readonly User currentUser;

        public UserDetailsWindow(User user)
        {
            InitializeComponent();
            currentUser = user;

            LoadUserDetails();
        }

        private void LoadUserDetails()
        {
            txtFirstName.Text = currentUser.firstName;
            txtLastName.Text = currentUser.lastName;
            txtRole.Text = currentUser.role;

            // Masked password display (first and last characters)
            if (!string.IsNullOrEmpty(currentUser.password))
            {
                string pwd = currentUser.password;
                if (pwd.Length > 2)
                {
                    txtPassword.Text = $"{pwd[0]}{new string('*', pwd.Length - 2)}{pwd[^1]}";
                }
                else
                {
                    txtPassword.Text = new string('*', pwd.Length);
                }
            }
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(currentUser);
            WindowManager.Open(this, changePasswordWindow);
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var logoutWindow = new LogoutWindow();
            WindowManager.Open(this, logoutWindow);
        }
    }
}
