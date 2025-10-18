using EMS.Core.Models;
using EMS.Core.Repositories;
using System.Windows;

namespace EMS.Views
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly User _currentUser;
        private readonly UserRepository _userRepo = new UserRepository();

        public ChangePasswordWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            var current = txtCurrentPassword.Password;
            var newPass = txtNewPassword.Password;
            var confirm = txtConfirmNewPassword.Password;

            if (!_userRepo.ValidateUser(_currentUser.userName, current))
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            _userRepo.ChangePassword(_currentUser.userID, newPass);
            MessageBox.Show("Password changed successfully.");
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
