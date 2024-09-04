using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        // The service layer is responsible for processing business logic.
        // Right now, the service layer is just calling the repository layer.
        // The service layer will call the repository layer to do the actual CRUD operations.
        // The service layer will return the data to the endpoint (controller).
        private readonly IWeatherForecastRepository _weatherForecastRepo;

        // This constructor is used for dependency injection.
        // We are injecting the IWeatherForecastRepository interface into the WeatherForecastService class.
        // We inject the repository interface instead of the actual repository class.
        // This is because we can easily mock the interface for unit testing.
        // It also makes our code more flexible and easier to maintain.
        // The type of DI used here is called constructor injection.
        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepo)
        {
            _weatherForecastRepo = weatherForecastRepo;
        }

        // async means that the method is asynchronous.
        // async methods can be awaited using the await keyword.
        // async methods return a Task or Task<T>.
        // Task represents an asynchronous operation that can return a value.
        // Task<T> is a task that returns a value.
        // To get the value, we use the await keyword.
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
