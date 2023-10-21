using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class RegionViewModel
    {
        public RegionViewModel(Region region)
        {
            ID = region.ID;
            LocalizedName = region.LocalizedName;
            EnglishName = region.EnglishName;
        }

        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }
}
