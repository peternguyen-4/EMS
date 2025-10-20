using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Globalization;
using System.Windows;

namespace EMS.Views
{
    public partial class AddSoilRecordWindow : Window
    {
        private readonly SoilRepository _soilRepo = new SoilRepository();

        public AddSoilRecordWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var record = new SoilData
                {
                    date = DatePicker.SelectedDate ?? DateTime.Today,
                    pH = float.Parse(pHTextBox.Text, CultureInfo.InvariantCulture),
                    moisture = float.Parse(MoistureTextBox.Text, CultureInfo.InvariantCulture),
                    firmness = int.Parse(FirmnessTextBox.Text, CultureInfo.InvariantCulture),
                    density = float.Parse(DensityTextBox.Text, CultureInfo.InvariantCulture),
                    nitrogen = float.Parse(NitrogenTextBox.Text, CultureInfo.InvariantCulture),
                    organicMatter = float.Parse(OrganicMatterTextBox.Text, CultureInfo.InvariantCulture),
                    microbiology = MicrobiologyTextBox.Text,
                    contaminants = ContaminantsTextBox.Text
                };

                _soilRepo.Add(record);

                MessageBox.Show("Record added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // closes the window
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
