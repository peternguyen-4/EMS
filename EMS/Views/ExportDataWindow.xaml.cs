using EMS.Core.Repositories;
using EMS.Core.Repositories.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace EMS.Views
{
    public partial class ExportDataWindow : Window
    {
        private readonly List<IExportable> _exportables;

        public ExportDataWindow()
        {
            InitializeComponent();

            // Initialize repositories that support export
            _exportables = new List<IExportable>
            {
                new SoilRepository(),
                new WaterRepository(),
                new SpeciesDataRepository()
            };

            // Populate dropdown with readable names
            DataTypeDropdown.ItemsSource = _exportables.Select(e => e.Name).ToList();

            // Optionally auto-select the first entry
            if (DataTypeDropdown.Items.Count > 0)
                DataTypeDropdown.SelectedIndex = 0;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataTypeDropdown.SelectedItem == null)
            {
                ShowStatus("Please select a data type to export.");
                return;
            }

            var selectedName = DataTypeDropdown.SelectedItem.ToString();
            var repo = _exportables.FirstOrDefault(r => r.Name == selectedName);

            if (repo == null)
            {
                ShowStatus("Could not find a matching data repository.");
                return;
            }

            // Ask user where to save
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"{selectedName}_Export_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var data = repo.GetAllForExport();
                    ExportToCsv(data, saveFileDialog.FileName);
                    ShowStatus($"Export successful: {saveFileDialog.FileName}");
                }
                catch (Exception ex)
                {
                    ShowStatus($"Export failed: {ex.Message}");
                }
            }
            else
            {
                ShowStatus("Export cancelled.");
            }
        }

        private void ExportToCsv(IEnumerable<object> data, string filePath)
        {
            if (data == null || !data.Any())
                throw new Exception("No data available to export.");

            var firstItem = data.First();
            var properties = firstItem.GetType().GetProperties();

            var sb = new StringBuilder();

            // Header
            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            // Rows
            foreach (var item in data)
            {
                var values = properties.Select(p =>
                {
                    var val = p.GetValue(item);
                    if (val == null)
                        return "";
                    // Escape commas or quotes if necessary
                    var stringVal = val.ToString() ?? "";
                    if (stringVal.Contains(",") || stringVal.Contains("\""))
                    {
                        stringVal = $"\"{stringVal.Replace("\"", "\"\"")}\"";
                    }
                    return stringVal;
                });

                sb.AppendLine(string.Join(",", values));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void ShowStatus(string message)
        {
            MessageBox.Show(message);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
