using EMS.Core.Models;
using EMS.Core.Repositories;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EMS.Views
{
    public partial class SpeciesManagementWindow : Window
    {
        private readonly SpeciesDataRepository _speciesDataRepo = new SpeciesDataRepository();
        private readonly SpeciesRepository _speciesRepo = new SpeciesRepository();
        private ObservableCollection<SpeciesData> _items = new ObservableCollection<SpeciesData>();
        private bool _isRefreshing = false;

        public SpeciesManagementWindow()
        {
            InitializeComponent();

            CartesianChart.Series = new SeriesCollection();
            CartesianChart.AxisX.Clear();
            CartesianChart.AxisY.Clear();
        }

        #region Window lifecycle
        private void SpeciesManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSpeciesComboBox(); // populate species dropdown first

            // Select first species
            if (SpeciesComboBox.Items.Count > 0)
                SpeciesComboBox.SelectedIndex = 0;

            ChartTypeComboBox.SelectedIndex = 0;

            // Load data for selected species
            LoadFilteredData();
            RefreshCharts();
        }
        #endregion

        #region Data loading / filtering

        private void LoadFilteredData()
        {
            if (SpeciesComboBox.SelectedItem is Species selectedSpecies)
            {
                var filteredData = _speciesDataRepo.GetDataBySpecies(selectedSpecies.speciesID);
                _items.Clear();
                foreach (var sd in filteredData.OrderByDescending(x => x.date))
                    _items.Add(sd);

                SpeciesDataGrid.ItemsSource = _items;
            }
        }

        private IEnumerable<SpeciesData> GetFilteredItems()
        {
            var query = _items.AsEnumerable();

            // Species filter
            if (SpeciesComboBox.SelectedItem is Species selected)
                query = query.Where(x => x.speciesID == selected.speciesID);

            // Date filters
            if (StartDatePicker.SelectedDate.HasValue)
                query = query.Where(x => x.date.Date >= StartDatePicker.SelectedDate.Value.Date);

            if (EndDatePicker.SelectedDate.HasValue)
                query = query.Where(x => x.date.Date <= EndDatePicker.SelectedDate.Value.Date);

            return query.OrderBy(x => x.date);
        }

        #endregion

        #region Chart helpers
        private void RefreshCharts()
        {
            if (_isRefreshing) return;
            _isRefreshing = true;

            try
            {
                var items = GetFilteredItems().ToList();
                var selected = (ChartTypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Population Count";

                switch (selected)
                {
                    case "Population Count":
                        ShowTimeSeries(items, x => x.populationCount, "Population Count");
                        break;
                    case "Scat Count":
                        ShowTimeSeries(items, x => x.scatCount, "Scat Count");
                        break;
                    case "Reproductive Factor":
                        ShowTimeSeries(items, x => x.reproductiveFactor, "Reproductive Factor");
                        break;
                }
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void ShowTimeSeries(IEnumerable<SpeciesData> data, Func<SpeciesData, double> selector, string title)
        {
            if (CartesianChart == null) return;

            CartesianChart.Series.Clear();
            CartesianChart.AxisX.Clear();
            CartesianChart.AxisY.Clear();

            var points = data.Select(d => new DateTimePoint(d.date, selector(d))).ToList();
            if (points.Count == 1)
            {
                var single = points[0];
                points.Add(new DateTimePoint(single.DateTime.AddMinutes(1), single.Value));
            }

            CartesianChart.Series.Add(new LineSeries
            {
                Title = title,
                Values = new ChartValues<DateTimePoint>(points),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 6,
                DataLabels = ShowLabelsCheckbox.IsChecked == true
            });

            CartesianChart.AxisX.Add(new Axis
            {
                LabelFormatter = val => new DateTime((long)val).ToString("yyyy-MM-dd"),
                Separator = new LiveCharts.Wpf.Separator()
            });

            CartesianChart.AxisY.Add(new Axis { Title = title });
            CartesianChart.LegendLocation = ShowLegendCheckbox.IsChecked == true ? LegendLocation.Right : LegendLocation.None;
        }
        #endregion

        #region UI events
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCharts();
            SpeciesDataGrid.ItemsSource = GetFilteredItems().ToList();
        }

        private void AddNewSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            var addSpeciesWindow = new AddNewSpeciesWindow();
            if (addSpeciesWindow.ShowDialog() == true)
            {
                LoadSpeciesComboBox(); // refresh species list
            }
        }

        private void AddSpeciesDataButton_Click(object sender, RoutedEventArgs e)
        {
            var addDataWindow = new AddSpeciesDataWindow();
            if (addDataWindow.ShowDialog() == true)
            {
                LoadFilteredData();
                RefreshCharts();
            }
        }

        private void LoadSpeciesComboBox()
        {
            var speciesList = _speciesRepo.GetAll();
            SpeciesComboBox.ItemsSource = speciesList;
            SpeciesComboBox.DisplayMemberPath = "speciesName";
            if (speciesList.Count > 0) SpeciesComboBox.SelectedIndex = 0;
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var logoutWindow = new LogoutWindow();
            WindowManager.Open(this, logoutWindow);
        }

        private void ChartTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshCharts();
        }

        private void ChartOptions_Changed(object sender, RoutedEventArgs e)
        {
            RefreshCharts();
        }

        private void SpeciesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFilteredData();
            RefreshCharts();
        }

        #endregion
    }
}
