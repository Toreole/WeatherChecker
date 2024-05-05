using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Configuration;

namespace WeatherChecker.Data;

public class WeatherDBContext : DbContext
{
	public DbSet<ActualWeatherData> MeasuredWeatherData { get; set; }
	public DbSet<Forecast> Forecasts { get; set; }
	public DbSet<ForecastWeatherData> ForecastWeatherData { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<UserPreferences> UserPreferences { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var settings = ConfigurationManager.AppSettings;

		var connector = new MySqlConnectionStringBuilder
		{
			Server = settings.Get("server"),
			Port = uint.Parse(settings.Get("port") ?? throw new Exception("key 'port' must be set in app config")),
			Database = settings.Get("database"),
			UserID = settings.Get("user"),
			Password = settings.Get("password"),
		};

		optionsBuilder.UseMySql(
			connector.ToString(),
			new MariaDbServerVersion("10.3.39") //its what im using rn deal with it
			);
		base.OnConfiguring(optionsBuilder);
	}
}
