using AuthAPI.Database;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<UserDbContext>();

var redisLocation = Environment.GetEnvironmentVariable("REDIS_LOCATION");

if (string.IsNullOrEmpty(redisLocation))
{
    redisLocation = "localhost:6379";
}

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { redisLocation }
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
