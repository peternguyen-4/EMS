using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Windows;

namespace EMS.Views
{
    public partial class DetailWindow : Window
    {
        private NotificationsRepository _notificationRepo;
        private UserTaskRepository _taskRepo;

        private Notification _notification;
        private UserTask _task;

        public string title { get; private set; }
        public string description { get; private set; }
        public DateTime creationDate { get; private set; }

        public DetailWindow(Notification notification)
        {
            InitializeComponent();
            _notification = notification;
            _notificationRepo = new NotificationsRepository();

            title = notification.title;
            description = notification.description;
            creationDate = notification.creationDate;

            this.Title = $"View Notification";

            DataContext = this;
        }

        public DetailWindow(UserTask task)
        {
            InitializeComponent();
            _task = task;
            _taskRepo = new UserTaskRepository();

            title = task.title;
            description = task.description;
            creationDate = task.creationDate;

            this.Title = $"View Task";

            DataContext = this;
        }

        private void TerminateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_notification != null)
                {
                    _notificationRepo.Terminate(_notification.notificationID);
                }
                else if (_task != null)
                {
                    _taskRepo.Terminate(_task.taskID);
                }

                MessageBox.Show("Entry terminated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // indicates parent should refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to terminate entry: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
