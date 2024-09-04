using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Interfaces
{
    public interface IWeatherForecastService
    {
        // The service layer is responsible for processing business logic.
        // The service layer will call the repository layer to do the actual CRUD operations.
        Task<List<WeatherForecast>> GetWeatherForecastAsync();
        Task<WeatherForecast> GetWeatherForecastByIdAsync(int id);
        Task<WeatherForecast> CreateWeatherForecastAsync(WeatherForecast weatherForecast);
        Task<WeatherForecast> UpdateWeatherForecastAsync(int id, WeatherForecast weatherForecast);
        Task<WeatherForecast> DeleteWeatherForecastAsync(int id);
    }
}
