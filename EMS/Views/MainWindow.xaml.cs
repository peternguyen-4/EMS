using EMS.Core.Models;
using EMS.Core.Repositories;
using EMS.Views;
using System.Windows;

namespace EMS
{
    public partial class MainWindow : Window
    {
        private readonly User currentUser;
        private NotificationsRepository _notificationRepo = new NotificationsRepository();
        private UserTaskRepository _taskRepo = new UserTaskRepository();

        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            txtWelcome.Text = $"Welcome, {currentUser.firstName} {currentUser.lastName} to the Environment Management System!";

            LoadNotifications();
            LoadTasks();
        }

        private void LoadNotifications()
        {
            dgNotifications.ItemsSource = _notificationRepo.GetActiveByUser(currentUser.userID);
        }

        private void LoadTasks()
        {
            dgTasks.ItemsSource = _taskRepo.GetActiveByUser(currentUser.userID);
        }

        // Button click handlers - open new windows for modules
        private void BtnSoil_Click(object sender, RoutedEventArgs e)
        {
            var window = new SoilManagementWindow();
            WindowManager.Open(this, window);
        }

        private void BtnWater_Click(object sender, RoutedEventArgs e)
        {
            var window = new WaterManagementWindow();
            WindowManager.Open(this, window);
        }

        private void BtnSpecies_Click(object sender, RoutedEventArgs e)
        {
            //var window = new SpeciesManagementWindow(currentUser);
            //WindowManager.Open(this, window);
        }

        private void BtnNotifications_Click(object sender, RoutedEventArgs e)
        {
            //var window = new NotificationsWindow(currentUser);
            //WindowManager.Open(this, window);
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            //var window = new TasksWindow(currentUser);
            //WindowManager.Open(this, window);
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            //var window = new ExportDataWindow(currentUser);
            //WindowManager.Open(this, window);
        }

        private void BtnUserDetails_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserDetailsWindow(currentUser);
            WindowManager.Open(this, window);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var logoutWindow = new LogoutWindow();
            WindowManager.Open(this, logoutWindow);
        }

        private void dgNotifications_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgNotifications.SelectedItem is Notification selected)
            {
                var detail = new DetailWindow(selected) { Owner = this };
                if (detail.ShowDialog() == true)
                {
                    // Refresh notifications if terminated
                    dgNotifications.ItemsSource = _notificationRepo.GetActiveByUser(currentUser.userID);
                }
            }
        }

        private void dgTasks_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgTasks.SelectedItem is UserTask selected)
            {
                var detail = new DetailWindow(selected) { Owner = this };
                if (detail.ShowDialog() == true)
                {
                    // Refresh tasks if terminated
                    dgTasks.ItemsSource = _taskRepo.GetActiveByUser(currentUser.userID);
                }
            }
        }
    }
}
