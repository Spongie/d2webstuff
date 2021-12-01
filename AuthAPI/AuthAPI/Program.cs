using AuthAPI.Database;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<UserDbContext>();

var redisConfig = builder.Configuration.GetSection("Redis");
var redisConnectionString = $"{redisConfig["Server"]}:{redisConfig["Port"]},password={redisConfig["AccessKey"]},ssl={redisConfig["SSL"]}";

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
