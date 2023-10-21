using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class HourWeatherViewModel
    {
        public HourWeatherViewModel(HourWeather hourWeather)
        {
            DateTime = hourWeather.DateTime;
            WeatherIcon = hourWeather.WeatherIcon;
            IconPhrase = hourWeather.IconPhrase;
            IsDaylight = hourWeather.IsDaylight;
            Temperature = hourWeather.Temperature;

            DisplayText = "at " + DateTime.Hour + " will be " + Temperature.Value;
        }

        public DateTime DateTime { get; set; }
        public int WeatherIcon { get; set; }
        public string IconPhrase { get; set; }
        public bool IsDaylight { get; set; }
        public ShortTemperature Temperature { get; set; }

        public string DisplayText { get; set; }
    }
}
