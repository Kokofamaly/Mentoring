using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.Configuration;
using MongoDB.Driver;
using WordCardsApi.Infrastructure.Data;
using WordCardsApi.Infrastructure.Providers;
using WordCardsApi.Infrastructure.Settings;
using WordCardsApi.Models;
using WordCardsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<MongoDbContext>();


builder.Services.AddScoped<JwtService>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LearningSessionService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<UserWordService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<LearningSessionProvider>();
builder.Services.AddScoped<RefreshTokenProvider>();
builder.Services.AddScoped<SessionWordProvider>();
builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<UserWordProvider>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapGet("/users", async (MongoDbContext context) => await context.Users.Find(x => true).ToListAsync()).AllowAnonymous();
}

app.Run();
