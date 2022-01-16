using ShipVista.DAO;
using ShipVista.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});

builder.Services.AddDbContext<OfficeContext>(options =>
            options.UseNpgsql("Host=batyr.db.elephantsql.com;Database=xemrumym;Username=xemrumym;Password=" + ShipVista.Constants.Constants.DB_PASSWORD));

builder.Services.AddTransient<IPlantDataAccess, PlantDataAcces>();

builder.Services.AddTransient<IPlantService, PlantService>();

builder.Services.AddTransient<IUserDataAccess, UserDataAccess>();

builder.Services.AddTransient<IUserService, UserService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
