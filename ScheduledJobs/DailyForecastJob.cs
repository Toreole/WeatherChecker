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
			try
			{
				var forecast = new ForecastWeatherData()
				{
					Humidity = hourlyData.Relativehumidity_2m?[i] ?? -1,
					Temperature = hourlyData.Temperature_2m?[i] ?? -1000,
					Windspeed = hourlyData.Windspeed_10m?[i] ?? -1,
					WeatherCode = (WeatherCode?)hourlyData.Weathercode?[i] ?? WeatherCode.UNKNOWN,
					ForecastTimestamp = now.ToLocalTime(),
					PredictionTimestamp = DateTimeOffset.Parse(hourlyData.Time[i]).ToLocalTime()
				};
				dbContext.Forecasts.Add(forecast);
			} 
			catch (Exception ex)
			{
				Console.WriteLine($"hourly time in unexpected format: {hourlyData.Time[0]}");
			}
		}
		await dbContext.SaveChangesAsync();
	}
}
