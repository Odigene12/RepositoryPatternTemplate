using Microsoft.EntityFrameworkCore;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Data
{
    public class WeatherForecastDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
        {
        }

        // seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().HasData(
                new WeatherForecast { Id = 1, WeatherDate = DateTime.UtcNow, TemperatureC = 25, Summary = "Hot" },
                new WeatherForecast { Id = 2, WeatherDate = DateTime.UtcNow, TemperatureC = 20, Summary = "Warm" },
                new WeatherForecast { Id = 3, WeatherDate = DateTime.UtcNow, TemperatureC = 15, Summary = "Cool" },
                new WeatherForecast { Id = 4, WeatherDate = DateTime.UtcNow, TemperatureC = 10, Summary = "Cold" },
                new WeatherForecast { Id = 5, WeatherDate = DateTime.UtcNow, TemperatureC = 5, Summary = "Freezing" }
            );
        }
    }
}
