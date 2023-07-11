using asp.netCoreWebApi;
using asp.netCoreWebApi.Data;
using asp.netCoreWebApi.logging;
using asp.netCoreWebApi.Repository;
using asp.netCoreWebApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
);
// player repo
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
// player number repo
builder.Services.AddScoped<IPlayerNumberRepository, PlayerNumberRepository>();
// auto mapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/playerLogs.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//builder.Host.UseSerilog();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddSingleton<ILogging, Loggingv2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
