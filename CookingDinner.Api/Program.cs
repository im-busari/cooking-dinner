using CookingDiner.Api.Errors;
using CookingDiner.Api.Filters;
using CookingDiner.Api.Middleware;
using CookingDinner.Application;
using CookingDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Layer DI
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

// Overrider problemDetailsFactory implementation
builder.Services.AddSingleton<ProblemDetailsFactory, CookingDinnerProblemDetailsFactory>();

//  Error Handling using FilterAttributes
// builder.Services.AddControllers(options =>
// {
//     options.Filters.Add<ErrorHandlingFilterAttribute>();
// }); // NEW

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
// app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseExceptionHandler("/api/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); // NEW

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}