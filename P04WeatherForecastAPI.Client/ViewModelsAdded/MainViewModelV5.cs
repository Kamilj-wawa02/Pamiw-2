using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModelV5 : ObservableObject
    {
        private CityViewModel _selectedCity;
        private RegionViewModel _selectedRegion;

        [ObservableProperty]
        private WeatherViewModel weatherView;

        [ObservableProperty]
        private WeatherViewModel weatherPast24HoursView;

        [ObservableProperty]
        private HourWeatherViewModel hourWeatherIn1HourView;

        private readonly IAccuWeatherService _accuWeatherService;
        private readonly FavoriteCitiesView _favoriteCitiesView;
        private readonly FavoriteCityViewModel _favoriteCityViewModel;
        //public ICommand LoadCitiesCommand { get;  }


        public MainViewModelV5(IAccuWeatherService accuWeatherService, FavoriteCityViewModel favoriteCityViewModel, FavoriteCitiesView favoriteCitiesView)
        {
            _favoriteCitiesView = favoriteCitiesView;
            _favoriteCityViewModel = favoriteCityViewModel;
            // _serviceProvider= serviceProvider; 
            //LoadCitiesCommand = new RelayCommand(x => LoadCities(x as string));
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>();
            TemperaturesFor12h = new ObservableCollection<HourWeatherViewModel>();
            Regions = new ObservableCollection<RegionViewModel>();
            Countries = new ObservableCollection<RegionViewModel>();
        }


        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }

        public RegionViewModel SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                _selectedRegion = value;
                OnPropertyChanged();
                LoadCountries();
            }
        }

        public ObservableCollection<HourWeatherViewModel> TemperaturesFor12h { get; set; }

        private async void LoadWeather()
        {
            if(SelectedCity != null)
            {
                WeatherView = new WeatherViewModel(await _accuWeatherService.GetCurrentConditions(SelectedCity.Key));
                WeatherPast24HoursView = new WeatherViewModel(await _accuWeatherService.GetHistoricalCurrentConditionsPast24Hours(SelectedCity.Key));
                HourWeatherIn1HourView = new HourWeatherViewModel(await _accuWeatherService.Get1HourOfHourlyForecats(SelectedCity.Key));
                
                var temperaturesFor12h = await _accuWeatherService.Get12HoursOfHourlyForecats(SelectedCity.Key);
                TemperaturesFor12h.Clear();
                foreach (var temperature in temperaturesFor12h)
                    TemperaturesFor12h.Add(new HourWeatherViewModel(temperature));
            }
        } 

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities) 
                Cities.Add(new CityViewModel(city));
        }

        public ObservableCollection<RegionViewModel> Regions { get; set; }

        [RelayCommand]
        public async void LoadRegions()
        {
            var regions = await _accuWeatherService.GetRegions();
            Regions.Clear();
            foreach (var region in regions)
                Regions.Add(new RegionViewModel(region));
        }

        public ObservableCollection<RegionViewModel> Countries { get; set; }

        [RelayCommand]
        public async void LoadCountries()
        {
            var countries = await _accuWeatherService.GetCountryList(SelectedRegion.LocalizedName);
            Countries.Clear();
            foreach (var country in countries)
                Countries.Add(new RegionViewModel(country));
        }

        [RelayCommand]
        public void OpenFavotireCities()
        {
            //var favoriteCitiesView = new FavoriteCitiesView();
            _favoriteCityViewModel.SelectedCity = new FavoriteCity() { Name = "Warsaw" };
            _favoriteCitiesView.Show();
        }
    }
}
