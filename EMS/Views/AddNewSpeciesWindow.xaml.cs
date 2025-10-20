using EMS.Core.Models;
using EMS.Core.Repositories;
using System;
using System.Windows;

namespace EMS.Views
{
    public partial class AddNewSpeciesWindow : Window
    {
        private readonly SpeciesRepository _speciesRepo = new SpeciesRepository();

        public AddNewSpeciesWindow()
        {
            InitializeComponent();
            LoadSpeciesGrid();
        }
        
        private void LoadSpeciesGrid()
        {
            var speciesList = _speciesRepo.GetAll();
            SpeciesDataGrid.ItemsSource = speciesList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = SpeciesNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Species name cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_speciesRepo.GetAll().Any(s => s.speciesName.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("This species already exists.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newSpecies = new Species { speciesName = name };
            _speciesRepo.Add(newSpecies);

            MessageBox.Show("Species added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
