using WebApplicationGalaxyDefender.Repository;
using WebApplicationGalaxyDefender.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<CharacterRepos>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
