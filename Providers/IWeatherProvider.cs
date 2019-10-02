using System.Collections.Generic;
using sandalphon.Models;

namespace sandalphon.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
