using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Windows;

namespace EMS.Views
{
    public partial class AddEntryWindow : Window
    {
        private readonly string _entryType; // "Notification" or "Task"
        private readonly int _userID;

        private NotificationsRepository _notificationRepo;
        private UserTaskRepository _taskRepo;

        public AddEntryWindow(string entryType, int userID)
        {
            InitializeComponent();

            _entryType = entryType;
            _userID = userID;

            if (_entryType == "Notification") _notificationRepo = new NotificationsRepository();
            else if (_entryType == "Task") _taskRepo = new UserTaskRepository();

            this.Title = $"Add {_entryType}";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Please enter a title.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_entryType == "Notification")
            {
                var notification = new Notification
                {
                    userID = _userID,
                    title = TitleTextBox.Text,
                    description = DescriptionTextBox.Text,
                    creationDate = DateTime.Now,
                    terminationDate = null,
                    isActive = true
                };
                _notificationRepo.Add(notification);
            }
            else if (_entryType == "Task")
            {
                var task = new UserTask
                {
                    userID = _userID,
                    title = TitleTextBox.Text,
                    description = DescriptionTextBox.Text,
                    creationDate = DateTime.Now,
                    isActive = true
                };
                _taskRepo.Add(task);
            }

            MessageBox.Show($"{_entryType} added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
