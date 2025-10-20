using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Globalization;
using System.Windows;

namespace EMS.Views
{
    public partial class AddWaterRecordWindow : Window
    {
        private readonly WaterRepository _waterRepo = new WaterRepository();

        public AddWaterRecordWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var record = new WaterData
                {
                    date = DatePicker.SelectedDate ?? DateTime.Today,
                    pH = float.Parse(pHTextBox.Text, CultureInfo.InvariantCulture),
                    dissolvedOxygen = float.Parse(DissolvedOxygenTextBox.Text, CultureInfo.InvariantCulture),
                    salinity = float.Parse(SalinityTextBox.Text, CultureInfo.InvariantCulture),
                    turbidity = float.Parse(TurbidityTextBox.Text, CultureInfo.InvariantCulture),
                    hardness = float.Parse(HardnessTextBox.Text, CultureInfo.InvariantCulture),
                    eutrophicPotential = float.Parse(EutrophicPotentialTextBox.Text, CultureInfo.InvariantCulture),
                    microbiology = MicrobiologyTextBox.Text,
                    contaminants = ContaminantsTextBox.Text
                };

                _waterRepo.Add(record);

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
