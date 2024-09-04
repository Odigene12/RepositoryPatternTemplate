using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepo;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepo) 
        {
            _weatherForecastRepo = weatherForecastRepo;
        }

        public async Task<WeatherForecast> CreateWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            return await _weatherForecastRepo.CreateWeatherForecastAsync(weatherForecast);
        }

        public async Task<WeatherForecast> DeleteWeatherForecastAsync(int id)
        {
            return await _weatherForecastRepo.DeleteWeatherForecastAsync(id);
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
        {
            return await _weatherForecastRepo.GetWeatherForecastAsync();
        }

        public async Task<WeatherForecast> GetWeatherForecastByIdAsync(int id)
        {
            return await _weatherForecastRepo.GetWeatherForecastByIdAsync(id);
        }

        public async Task<WeatherForecast> UpdateWeatherForecastAsync(int id, WeatherForecast weatherForecast)
        {
            return await _weatherForecastRepo.UpdateWeatherForecastAsync(id, weatherForecast);
        }
    }
}
