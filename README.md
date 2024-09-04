# Repository Pattern & Dependency Injection in .NET

## Table of Contents
- [What is the Repository Pattern?](#what-is-the-repository-pattern)
- [Benefits of the Repository Pattern](#benefits-of-the-repository-pattern)
- [What is Dependency Injection?](#what-is-dependency-injection)
- [How Does Dependency Injection Work in .NET Core?](#how-does-dependency-injection-work-in-net-core)
- [Example of Dependency Injection in a .NET Core Project](#example-of-dependency-injection-in-a-net-core-project)

## What is the Repository Pattern?

The **Repository Pattern** is a design pattern used to separate the logic that interacts with the database (or any data source) from the business logic in your application. It acts as an intermediary between the data access layer and the business logic layer.

### Key Points:
- The repository provides a set of methods to query, save, and delete data.
- It allows you to **decouple** your business logic from data access logic.
- If you want to change the underlying data source (for example, from SQL Server to MongoDB), you only need to modify the repository, not the rest of your application.

### Example of a Repository Interface:

```csharp
public interface IWeatherForecastRepository
{
    Task<List<WeatherForecast>> GetWeatherForecastAsync();
    Task<WeatherForecast> GetWeatherForecastByIdAsync(int id);
    Task<WeatherForecast> CreateWeatherForecastAsync(WeatherForecast weatherForecast);
    Task<WeatherForecast> UpdateWeatherForecastAsync(int id, WeatherForecast weatherForecast);
    Task<WeatherForecast> DeleteWeatherForecastAsync(int id);
}
```

### What is an Interface?

An **interface** is like a blueprint for a class. It tells a class what it needs to do, but not how to do it. An interface defines a list of methods or properties that a class must have, but it doesn't include any actual code or details on how these methods work.

### Key Points:
- **Defines "what", not "how"**: It specifies the methods and properties a class should have but leaves the actual details of how they work to the class that implements the interface.
- **No code in an interface**: Interfaces only define the names of the methods and properties, but no code inside them.
- **Multiple interfaces**: A class can follow multiple blueprints (interfaces) at the same time, allowing it to do different things.
- **Helps organize code**: Interfaces make code more flexible and easier to manage, especially when working with large projects.

### Example:
```csharp
public interface IAnimal
{
    void Speak();
}
```

This interface `IAnimal` says that any class implementing it must have a `Speak` method, but it doesn't say what "Speak" will actually do.

### Benefits of Using Interfaces:
- **Flexibility**: You can swap out different implementations of the same interface without changing other parts of your code.
- **Easier testing**: You can use interfaces to test your code more easily by swapping in fake versions of a class.
- **Works with dependency injection**: Interfaces are useful when you need to inject different versions of a class into your application.

### Example of Implementing an Interface:
```csharp
public class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Woof!");
    }
}
```

In this example, the `Dog` class follows the `IAnimal` blueprint and provides the details for the `Speak` method by making the dog say "Woof!".

### Example of a Repository Implementation:
```csharp
public class WeatherForecastRepository : IWeatherForecastRepository
{
     private readonly WeatherForecastDbContext _context;
    
     public WeatherForecastRepository(WeatherForecastDbContext context)
     {
         _context = context;
     }
    
     public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
     {
         return await _context.WeatherForecasts.ToListAsync();
     }
}
```

## Benefits of the Repository Pattern

- **Separation of Concerns**: Keeps your business logic separate from your data access logic.
- **Easier Testing**: You can mock the repository in unit tests to test your business logic without depending on the actual database.
- **Maintainability**: It’s easier to maintain and update the data access logic without affecting other parts of the application.

## What is Dependency Injection?

**Dependency Injection (DI)** is a technique where objects (dependencies) are provided to a class instead of the class creating them itself. This allows for **loose coupling** and makes it easier to change the implementation or mock dependencies for testing.

### Key Points:
- Instead of a class creating its own dependencies, they are injected by an external source (like a DI container).
- In .NET Core, the DI container is built into the framework, and you can use it to manage dependencies at runtime.
- DI makes testing and maintaining your code easier because you can replace real dependencies with mock ones during testing.

### Example Without Dependency Injection:
```csharp
public class WeatherService
{
    private readonly WeatherForecastRepository _repository;

    public WeatherService()
    {
        _repository = new WeatherForecastRepository();  // The class creates its own dependencies
    }

    public void GetWeatherData()
    {
        var data = _repository.GetForecastsAsync();
    }
}
```

### Example With Dependency Injection:
```csharp
public class WeatherService
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
}
```

## How Does Dependency Injection Work in .NET Core?

In .NET Core, Dependency Injection is provided out of the box. You register your services (like repositories, database contexts, etc.) with the DI container, and the framework automatically provides these dependencies when needed.

### Steps to Use Dependency Injection:

1. **Register Dependencies**: Register your services (like repositories) with the DI container in the `Program.cs` file.
   
2. **Inject Dependencies**: Use **constructor injection** in your classes to receive the dependencies.

### Types of DI Scopes:
- **Transient**: A new instance is created every time the service is requested.
- **Scoped**: A single instance is created for each HTTP request.
- **Singleton**: A single instance is created and shared throughout the application lifetime.

## Example of Dependency Injection in a .NET Core Project

Here’s an example of how to register and use Dependency Injection in a Minimal API project:

### Registering Dependencies in `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RepositoryPatternTemplateDbConnection");

// Register the ApplicationDbContext with the DI container
builder.Services.AddDbContext<WeatherForecastDbContext>(options => options.UseNpgsql(connectionString));

// Register the repository with the DI container
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

var app = builder.Build();
```

### Injecting Dependencies in Minimal API:

```csharp
// Here we are calling the extension method MapWeatherEndpoints() to map the weather endpoints.    
app.MapWeatherEndpoints();
app.Run();
```

### Explanation:
- **`builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();`**: This line registers the `WeatherForecastRepository` with the DI container. It tells .NET Core to inject an instance of `WeatherForecastRepository` whenever `IWeatherForecastRepository` is requested.
  
- In the `MapGet()` method, we request the `IWeatherForecastRepository`, and .NET Core automatically injects the correct repository instance.

### Unit Testing:
Here’s a summarized breakdown of the key differences between **service layer** and **repository layer** tests:

| **Aspect**                   | **Repository Layer Tests**                                  | **Service Layer Tests**                                  |
|------------------------------|-------------------------------------------------------------|----------------------------------------------------------|
| **Focus**                     | Tests data access logic (CRUD operations).                  | Tests business logic and coordination of multiple components (e.g., repositories). |
| **Level of Abstraction**      | Low-level, closer to the database.                          | Higher-level, focusing on the logic that uses data from repositories. |
| **Dependencies**              | Interacts with the data source, often mocking the `DbContext` or using an in-memory database. | Interacts with repositories and other services, usually mocking repositories. |
| **Tools Used**                | Mocks or in-memory databases for data access (e.g., `DbContext`). | Mocks repository interfaces (e.g., `IRepository`) to isolate the service logic. |
| **Typical Errors Tested**     | Data retrieval, saving, updating, or deleting operations.   | Validation errors, business rule violations, and service coordination issues. |
| **Example**                   | Tests if `GetById()` fetches data from the database correctly. | Tests if `CreateForecast()` validates input and calls the repository correctly. |

### Summary:
- **Repository layer tests** focus on ensuring correct data interactions with the database, often using tools like in-memory databases or `DbContext` mocks.
- **Service layer tests** focus on the business logic and how it interacts with repositories or other services, typically using mocked repositories to isolate service behavior.

## Conclusion

- **Repository Pattern** helps in separating the data access logic from the business logic.
- **Dependency Injection** makes it easy to manage dependencies and write testable, maintainable code.
- .NET Core provides built-in support for DI, making it simple to set up and use in your applications.
- **Unit Testing** Unit testing is important because it helps ensure that individual components of your code work correctly in isolation.

****This template doesn't show repository tests because it's a lot more involved**
