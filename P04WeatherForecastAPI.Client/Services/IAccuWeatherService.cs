using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);

        // ---

        Task<Weather> GetHistoricalCurrentConditionsPast24Hours(string cityKey);
        Task<HourWeather[]> Get12HoursOfHourlyForecats(string cityKey);
        Task<HourWeather> Get1HourOfHourlyForecats(string cityKey);
        Task<Region[]> GetRegions();
        Task<Region[]> GetCountryList(string regionKey);

    }
}
