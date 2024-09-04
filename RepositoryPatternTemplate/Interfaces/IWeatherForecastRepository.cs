using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Interfaces
{
    public interface IWeatherForecastRepository
    {
        // An interface is a contract that defines the signature of the functionality.
        // It defines a set of methods that a class that inherits the interface MUST implement.
        // The interface is a mechanism to achieve abstraction.
        // Interfaces can be used in unit testing to mock out the actual implementation.
        Task<List<WeatherForecast>> GetWeatherForecastAsync();
        Task<WeatherForecast> GetWeatherForecastByIdAsync(int id);
        Task<WeatherForecast> CreateWeatherForecastAsync(WeatherForecast weatherForecast);
        Task<WeatherForecast> UpdateWeatherForecastAsync(int id, WeatherForecast weatherForecast);
        Task<WeatherForecast> DeleteWeatherForecastAsync(int id);
    }
}
