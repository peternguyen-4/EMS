using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace EMS.Views
{
    public partial class AddSpeciesDataWindow : Window
    {
        private readonly SpeciesDataRepository _dataRepo = new SpeciesDataRepository();
        private readonly SpeciesRepository _speciesRepo = new SpeciesRepository();

        public AddSpeciesDataWindow()
        {
            InitializeComponent();
            DatePickerInput.SelectedDate = DateTime.Now;
            LoadSpecies();
        }

        private void LoadSpecies()
        {
            var speciesList = _speciesRepo.GetAll();
            SpeciesComboBox.ItemsSource = speciesList;
            SpeciesComboBox.DisplayMemberPath = "speciesName";
            SpeciesComboBox.SelectedIndex = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpeciesComboBox.SelectedItem is not Species selectedSpecies)
            {
                MessageBox.Show("Please select a species.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(PopulationTextBox.Text, out int population))
            {
                MessageBox.Show("Population Count must be a valid integer.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(ScatTextBox.Text, out int scat))
            {
                MessageBox.Show("Scat Count must be a valid integer.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(ReproductiveFactorTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float reproductive))
            {
                MessageBox.Show("Reproductive Factor must be a valid number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newData = new SpeciesData
            {
                speciesID = selectedSpecies.speciesID,
                date = DatePickerInput.SelectedDate ?? DateTime.Now,
                populationCount = population,
                scatCount = scat,
                reproductiveFactor = reproductive,
                knownHabitats = HabitatsTextBox.Text.Trim(),
                healthConcerns = HealthTextBox.Text.Trim(),
                additionalNotes = NotesTextBox.Text.Trim()
            };

            _dataRepo.Add(newData);

            MessageBox.Show("Species data added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
