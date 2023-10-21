using P04WeatherForecastAPI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private readonly MainViewModelV5 _viewModel;
        public MainWindow2(MainViewModelV5 viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
