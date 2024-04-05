using OpenMeteo;
using Quartz;
using WeatherChecker.Data;

namespace WeatherChecker.ScheduledJobs;

public class CurrentWeatherJob : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		OpenMeteoClient client = new();
		var result = await client.QueryAsync(OpenMeteoHelper.CurrentWeatherOptions);

		var currentData = result?.Current;
		if (currentData == null)
		{
			//Log that it has failed.
			return;
		}
		using WeatherDBContext dbContext = new();
		dbContext.MeasuredWeatherData.Add(new()
		{
			Humidity = currentData.Relativehumidity_2m ?? -1,
			Temperature = currentData.Temperature_2m ?? -1000,
			Windspeed = currentData.Windspeed_10m ?? -1,
			WeatherCode = (WeatherCode?)currentData.Weathercode ?? WeatherCode.UNKNOWN,
			Timestamp = DateTimeOffset.Now.ToLocalTime()
		});
		await dbContext.SaveChangesAsync();
	}
}

