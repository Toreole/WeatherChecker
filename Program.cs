using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using OpenMeteo;
using Quartz;
using Quartz.Impl;
using System.Configuration;
using System.Globalization;
using WeatherChecker.Data;
using WeatherChecker.Jobs;
using WeatherChecker.ScheduledJobs;

namespace WeatherChecker;

public class Program
{
	static void Main(string[] _)
		=> Run().GetAwaiter().GetResult();
	
	static async Task Run()
	{
		//force into en-US culture for standardizing to decimal numbers using a . 
		CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
		UpdateDatabaseSchema();
		await new WeatherCheckerService().Run();
		await Task.Delay(-1);
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
			return;
		}
		var migrator = db.GetInfrastructure().GetService<IMigrator>();
		foreach (var migration in db.Database.GetPendingMigrations())
		{
			Console.WriteLine($"Applying migration {migration}");
			migrator?.Migrate(migration);
		}
	}

}
