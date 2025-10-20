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
    public partial class SoilManagementWindow : Window
    {
        private readonly SoilRepository _soilRepo = new SoilRepository();

        // backing collection shown in DataGrid
        private ObservableCollection<SoilData> _items = new ObservableCollection<SoilData>();

        private bool _isRefreshing = false; // prevent recursive RefreshCharts

        public SoilManagementWindow()
        {
            InitializeComponent();

            // make chart defaults
            CartesianChart.Series = new SeriesCollection();
            CartesianChart.AxisX.Clear();
            CartesianChart.AxisY.Clear();

        }

        #region Window lifecycle

        private void SoilManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllData();
            ChartTypeComboBox.SelectedIndex = 0; // default: pH (Line)
            RefreshCharts();
        }

        #endregion

        #region Data loading / filtering

        private void LoadAllData()
        {
            _items.Clear();
            var all = _soilRepo.GetAll() ?? new List<SoilData>();
            foreach (var s in all.OrderByDescending(x => x.date))
                _items.Add(s);

            SoilDataGrid.ItemsSource = _items;
        }

        private IEnumerable<SoilData> GetFilteredItems()
        {
            var query = _items.AsEnumerable();

            if (StartDatePicker.SelectedDate.HasValue)
            {
                var start = StartDatePicker.SelectedDate.Value.Date;
                query = query.Where(x => x.date.Date >= start);
            }

            if (EndDatePicker.SelectedDate.HasValue)
            {
                var end = EndDatePicker.SelectedDate.Value.Date;
                query = query.Where(x => x.date.Date <= end);
            }

            return query.OrderBy(x => x.date);
        }

        #endregion

        #region Chart building helpers

        private void RefreshCharts()
        {
            if (_isRefreshing) return;
            _isRefreshing = true;

            try
            {
                var items = GetFilteredItems().ToList();
                var selected = (ChartTypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "pH";

                if (selected == "pH")
                    ShowTimeSeries(items, x => x.pH, "pH");
                else if (selected == "Firmness")
                    ShowTimeSeries(items, x => x.firmness, "Firmness");
                else if (selected == "Density")
                    ShowTimeSeries(items, x => x.density, "Density");
                else if (selected == "Moisture")
                    ShowTimeSeries(items, x => x.moisture, "Moisture");
                else if (selected == "Nitrogen")
                    ShowTimeSeries(items, x => x.nitrogen, "Nitrogen");
                else if (selected == "Organic Matter")
                    ShowTimeSeries(items, x => x.organicMatter, "Organic Matter");

            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void ShowTimeSeries(IEnumerable<SoilData> data, Func<SoilData, double> selector, string title)
        {
            if (CartesianChart == null) return;

            CartesianChart.Visibility = Visibility.Visible;

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

            CartesianChart.AxisY.Add(new Axis
            {
                Title = title
            });

            CartesianChart.LegendLocation = ShowLegendCheckbox.IsChecked == true ? LegendLocation.Right : LegendLocation.None;
        }

        #endregion

        #region UI events

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCharts();
            SoilDataGrid.ItemsSource = GetFilteredItems().ToList();
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddSoilRecordWindow
            {
                Owner = this
            };

            bool? result = addWindow.ShowDialog();
            if (result == true)
            {
                LoadAllData();
                RefreshCharts();
            }
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

        #endregion
    }
}
