using WebApplicationGalaxyDefender.Repository;
using WebApplicationGalaxyDefender.Service;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<CharacterRepos>();

builder.Services.AddScoped<GalaxyService>();
builder.Services.AddScoped<Galaxyrepos>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepos>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5501", "http://127.0.0.1:5501")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])), //Use [Authorize] over the endpoints you wanna protect
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => //Add (Policy = AdminOnly) in the  [Authorize]
    {
        policy.RequireClaim("isAdmin", "1");
    });
});

var app = builder.Build();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
