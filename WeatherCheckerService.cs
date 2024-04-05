using Quartz.Impl;
using Quartz;
using System.Configuration;
using WeatherChecker.ScheduledJobs;
using WeatherChecker.Jobs;

namespace WeatherChecker;

internal class WeatherCheckerService
{
	internal static readonly WeatherConfig config;

	private IScheduler? scheduler;

	static WeatherCheckerService()
	{
		var settings = ConfigurationManager.AppSettings;
		config = new WeatherConfig(
			float.Parse(settings.Get("latitude") ?? "0"),
			float.Parse(settings.Get("longitude") ?? "0"),
			int.Parse(settings.Get("forecast_days") ?? "0")
		);
	}

	public async Task Run(CancellationToken cancellationToken = default)
	{
		scheduler = await new StdSchedulerFactory()
			.GetScheduler(cancellationToken);

		await scheduler.Start(cancellationToken);
		//set up the jobs.
		var job = JobBuilder.Create<CurrentWeatherJob>()
			.Build();
		var trigger = TriggerBuilder.Create()
			.StartNow()
			.WithCronSchedule("0 0 * * * ?") //0 seconds, 0 minutes, every hour, every day
			.Build();
		await scheduler.ScheduleJob(job, trigger, cancellationToken);

		job = JobBuilder.Create<DailyForecastJob>()
			.Build();
		trigger = TriggerBuilder.Create()
			.StartNow()
			.WithCronSchedule("5 0 12 * * ?") //5 seconds, 0 minutes, 12:00, every day
			.Build();
		await scheduler.ScheduleJob(job, trigger, cancellationToken);

	}

	public async Task Shutdown() 
		=> await (scheduler?.Shutdown() ?? Task.Delay(1));

	internal readonly struct WeatherConfig
	{
		internal readonly float latitude, longitude;
		internal readonly int forecastDays;

		internal WeatherConfig(float lat, float lon, int days)
		{
			latitude = lat;
			longitude = lon;
			forecastDays = Math.Clamp(days, 1, 16);
		}
	}
}
