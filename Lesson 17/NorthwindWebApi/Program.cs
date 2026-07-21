using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using NorthwindWebApi.Models;
var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("NorthwindConnection");

builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
