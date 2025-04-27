using WebApplicationGalaxyDefender.Repository;
using WebApplicationGalaxyDefender.Service;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<CharacterRepos>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
