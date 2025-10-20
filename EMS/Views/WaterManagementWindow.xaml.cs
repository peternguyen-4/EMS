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
    public partial class WaterManagementWindow : Window
    {
        private readonly WaterRepository _waterRepo = new WaterRepository();
        private ObservableCollection<WaterData> _items = new ObservableCollection<WaterData>();
        private bool _isRefreshing = false;

        public WaterManagementWindow()
        {
            InitializeComponent();

            CartesianChart.Series = new SeriesCollection();
            CartesianChart.AxisX.Clear();
            CartesianChart.AxisY.Clear();
        }

        #region Window lifecycle

        private void WaterManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllData();
            ChartTypeComboBox.SelectedIndex = 0;
            RefreshCharts();
        }

        #endregion

        #region Data loading / filtering

        private void LoadAllData()
        {
            _items.Clear();
            var all = _waterRepo.GetAll() ?? new List<WaterData>();
            foreach (var w in all.OrderByDescending(x => x.date))
                _items.Add(w);

            WaterDataGrid.ItemsSource = _items;
        }

        private IEnumerable<WaterData> GetFilteredItems()
        {
            var query = _items.AsEnumerable();

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
                var selected = (ChartTypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "pH";

                switch (selected)
                {
                    case "pH":
                        ShowTimeSeries(items, x => x.pH, "pH");
                        break;
                    case "Dissolved Oxygen":
                        ShowTimeSeries(items, x => x.dissolvedOxygen, "Dissolved Oxygen");
                        break;
                    case "Salinity":
                        ShowTimeSeries(items, x => x.salinity, "Salinity");
                        break;
                    case "Turbidity":
                        ShowTimeSeries(items, x => x.turbidity, "Turbidity");
                        break;
                    case "Hardness":
                        ShowTimeSeries(items, x => x.hardness, "Hardness");
                        break;
                    case "Eutrophic Potential":
                        ShowTimeSeries(items, x => x.eutrophicPotential, "Eutrophic Potential");
                        break;
                }
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void ShowTimeSeries(IEnumerable<WaterData> data, Func<WaterData, double> selector, string title)
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

            CartesianChart.AxisY.Add(new Axis { Title = title });

            CartesianChart.LegendLocation = ShowLegendCheckbox.IsChecked == true ? LegendLocation.Right : LegendLocation.None;
        }

        #endregion

        #region UI events

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCharts();
            WaterDataGrid.ItemsSource = GetFilteredItems().ToList();
        }

        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWaterRecordWindow();
            if (addWindow.ShowDialog() == true)
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
