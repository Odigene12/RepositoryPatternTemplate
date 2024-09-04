using Moq;
using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;
using RepositoryPatternTemplate.Services;

namespace RepositoryPatternTemplate.Tests.Services
{
    public class WeatherForecastServiceTests
    {
        private readonly Mock<IWeatherForecastRepository> _mockWeatherForecastRepository;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _mockWeatherForecastRepository = new Mock<IWeatherForecastRepository>();
            _weatherForecastService = new WeatherForecastService(_mockWeatherForecastRepository.Object);
        }

        // Add tests here
        // Since our methods are async, we need to add the async keyword to our test methods.
        // We are returning Task instead of void because we are using async.
        [Fact]
        public async Task GetWeatherForecastAsync_WhenCalled_ReturnsWeatherForecastsAsync()
        {
            // Arrange
            var weatherForecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Id = 1, WeatherDate = DateTime.Now, TemperatureC = 25, Summary = "Sunny" },
                new WeatherForecast { Id = 2, WeatherDate = DateTime.Now, TemperatureC = 30, Summary = "Hot" }
            };

            _mockWeatherForecastRepository.Setup(x => x.GetWeatherForecastAsync()).ReturnsAsync(weatherForecasts);

            // Act
            var result = await _weatherForecastService.GetWeatherForecastAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
