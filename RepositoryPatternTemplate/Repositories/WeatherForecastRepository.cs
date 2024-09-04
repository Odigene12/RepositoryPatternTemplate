using Microsoft.EntityFrameworkCore;
using RepositoryPatternTemplate.Data;
using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        // The repository layer is responsible for CRUD operations.
        // This repository class implements the IWeatherForecastRepository interface.
        // Remember: the interface is a contract that defines methods that a class MUST implement.
        // The repository layer will call the database context to do the actual CRUD operations.
        // The repository layer will return the data to the service layer.
        private readonly WeatherForecastDbContext _context;

        public WeatherForecastRepository(WeatherForecastDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
        {
            return await _context.WeatherForecasts.ToListAsync();
        }

        public async Task<WeatherForecast> GetWeatherForecastByIdAsync(int id)
        {
            return await _context.WeatherForecasts.FindAsync(id);
        }

        public async Task<WeatherForecast> CreateWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();
            return weatherForecast;
        }

        public async Task<WeatherForecast> UpdateWeatherForecastAsync(int id, WeatherForecast weatherForecast)
        {
            var existingWeatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (existingWeatherForecast == null)
            {
                return null;
            }
            existingWeatherForecast.WeatherDate = weatherForecast.WeatherDate;
            existingWeatherForecast.TemperatureC = weatherForecast.TemperatureC;
            existingWeatherForecast.Summary = weatherForecast.Summary;
            await _context.SaveChangesAsync();
            return weatherForecast;
        }

        public async Task<WeatherForecast> DeleteWeatherForecastAsync(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return null;
            }
            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();
            return weatherForecast;
        }
    }
}
