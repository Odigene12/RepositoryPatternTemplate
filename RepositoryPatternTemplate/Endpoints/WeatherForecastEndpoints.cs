using RepositoryPatternTemplate.Interfaces;
using RepositoryPatternTemplate.Models;

namespace RepositoryPatternTemplate.Endpoints
{
    public static class WeatherForecastEndpoints
    {
        // The endpoint layer is responsible for handling HTTP requests.
        // The endpoint layer will call the service layer to process business logic.
        // The endpoint layer will return the data to the client.
        // The endpoint layer is the entry point for the client to access the application.
        // We must register this MapWeatherEndpoints method in the Program.cs file.
        // You can click the reference to see where it is registered in the Program.cs file.
        public static void MapWeatherEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/WeatherForecast").WithTags(nameof(WeatherForecast));

            group.MapGet("/", async (IWeatherForecastService weatherService) =>
            {
                return await weatherService.GetWeatherForecastAsync();
            })
            .WithName("GetWeatherForecast") // This is the name of the endpoint that Swagger will display.
            .WithOpenApi()
            .Produces<List<WeatherForecast>>(StatusCodes.Status200OK); // This is the response type and status code.

            group.MapGet("/{id}", async (IWeatherForecastService weatherService, int id) =>
            {
                var weather = await weatherService.GetWeatherForecastByIdAsync(id);
                return Results.Ok(weather);
            })
            .WithName("GetWeatherForecastById")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status200OK); // This is the response type and status code.

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
                return Results.Ok(weather);
            })
            .WithName("UpdateWeatherForecast")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            group.MapDelete("/{id}", async (IWeatherForecastService weatherService, int id) =>
            {
                var weather = await weatherService.DeleteWeatherForecastAsync(id);
                return Results.NoContent();
            })
            .WithName("DeleteWeatherForecast")
            .WithOpenApi()
            .Produces<WeatherForecast>(StatusCodes.Status204NoContent);
        }
    }
}
