using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Endpoints
{
    public static class WeatherForecastEndpoints
    {
        public static void MapWeatherEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/WeatherForecast").WithTags(nameof(WeatherForecast)); 

            group.MapGet("/", async (IWeatherForecastService weatherService) =>
            {
                return await weatherService.GetWeatherForecastAsync();
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi()
            .Produces<List<WeatherForecast>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/{id}", async (IWeatherForecastService weatherService, int id) =>
            {
                var weather = await weatherService.GetWeatherForecastByIdAsync(id);
                return weather is null ? Results.NotFound() : Results.Ok(weather);
            })
            .WithName("GetWeatherForecastById")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (IWeatherForecastService weatherService, WeatherForecast weatherForecast) =>
            {
                var weather = await weatherService.CreateWeatherForecastAsync(weatherForecast);
                return Results.Created($"/api/WeatherForecast/{weather.Id}", weather);
            })
            .WithName("CreateWeatherForecast")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", async (IWeatherForecastService weatherService, int id, WeatherForecast weatherForecast) =>
            {
                var weather = await weatherService.UpdateWeatherForecastAsync(id, weatherForecast);
                return weather is null ? Results.NotFound() : Results.Ok(weather);
            })
            .WithName("UpdateWeatherForecast")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id}", async (IWeatherForecastService weatherService, int id) =>
            {
                var weather = await weatherService.DeleteWeatherForecastAsync(id);
                return weather is null ? Results.NotFound() : Results.Created();
            })
            .WithName("DeleteWeatherForecast")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
