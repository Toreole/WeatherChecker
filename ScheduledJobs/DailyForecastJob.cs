using OpenMeteo;
using Quartz;
using WeatherChecker.Data;
using WeatherChecker.ScheduledJobs;

namespace WeatherChecker.Jobs;

public class DailyForecastJob : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		OpenMeteoClient client = new();

		var result = await client.QueryAsync(OpenMeteoHelper.DailyForecastOptions);

		var hourlyData = result?.Hourly;
		if (hourlyData == null)
		{
			//Log that it has failed.
			return;
		}
		using WeatherDBContext dbContext = new();
		var now = DateTimeOffset.Now;
		for (int i = 0; i < hourlyData.Time?.Length; i++)
		{
			dbContext.Forecasts.Add(new()
			{
				Humidity = hourlyData.Relativehumidity_2m?[i] ?? -1,
				Temperature = hourlyData.Temperature_2m?[i] ?? -1000,
				Windspeed = hourlyData.Windspeed_10m?[i] ?? -1,
				WeatherCode = (WeatherCode?)hourlyData.Weathercode?[i] ?? WeatherCode.UNKNOWN,
				ForecastTimestamp = now,
				PredictionTimestamp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(hourlyData.Time[i]))
			});
		}
		await dbContext.SaveChangesAsync();
	}
}
