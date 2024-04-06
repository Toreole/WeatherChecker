using OpenMeteo;
using Quartz;
using WeatherChecker.Data;

namespace WeatherChecker.ScheduledJobs;

public class CurrentWeatherJob : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		using WeatherDBContext dbContext = new();
		OpenMeteoClient client = new();
		var openMeteoSettings = OpenMeteoHelper.CurrentWeatherOptions;
		var locations = dbContext.Set<Location>();

		foreach (var location in locations)
		{
			openMeteoSettings.Latitude = location.Latitude;
			openMeteoSettings.Longitude = location.Longitude;
			var result = await client.QueryAsync(openMeteoSettings);

			var currentData = result?.Current;
			if (currentData == null)
			{
				//Log that it has failed.
				continue;
			}
			dbContext.MeasuredWeatherData.Add(new()
			{
				Humidity = currentData.Relativehumidity_2m ?? -1,
				Temperature = currentData.Temperature_2m ?? -1000,
				Windspeed = currentData.Windspeed_10m ?? -1,
				WeatherCode = (WeatherCode?)currentData.Weathercode ?? WeatherCode.UNKNOWN,
				Timestamp = DateTimeOffset.Now.ToLocalTime(),
				Location = location,
				LocationId = location.Id
			});
			await Task.Delay(500); //wait half a second between API calls.
		}
		await dbContext.SaveChangesAsync();
	}
}

