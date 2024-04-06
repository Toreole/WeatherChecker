using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using X10D.Hosting.DependencyInjection;
using WeatherChecker.Data;
using Discord.WebSocket;
using Discord.Interactions;
using WeatherChecker.DiscordBot;
using WeatherChecker.ScheduledJobs;

namespace WeatherChecker;

public class Program
{
	static async Task Main(string[] args)
	{
		//force into en-US culture for standardizing to decimal numbers using a . 
		CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
		Console.WriteLine("Checking Database");
		UpdateDatabaseSchema();

		HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
		builder.Logging.ClearProviders();
		//TODO: Add logging service.

		builder.Services.AddSingleton<DiscordSocketClient>();
		builder.Services.AddSingleton<InteractionService>();
		builder.Services.AddSingleton(new DiscordSocketConfig
		{
			GatewayIntents = Discord.GatewayIntents.AllUnprivileged
		});

		// builder.Services.AddSingleton<HttpClient>(); // idk if i need this

		//my own
		//builder.Services.AddSingleton<ConfigurationService>();
		builder.Services.AddHostedSingleton<BotService>();

		builder.Services.AddHostedService<WeatherCheckerService>();


		IHost app = builder.Build();
		await app.RunAsync();
	}

	/// <summary>
	/// Apply all pending migrations to the database.
	/// </summary>
	static void UpdateDatabaseSchema()
	{
		using WeatherDBContext db = new();
		Console.WriteLine(db.Database.ProviderName);
		if (db.Database.CanConnect() == false)
		{
			Console.WriteLine("Failed to connect to database.");
			throw new Exception("uhm");
		}
		var migrator = db.GetInfrastructure().GetService<IMigrator>();
		foreach (var migration in db.Database.GetPendingMigrations())
		{
			Console.WriteLine($"Applying migration {migration}");
			migrator?.Migrate(migration);
		}
	}

}
