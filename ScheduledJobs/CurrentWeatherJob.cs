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
			if (location.ActiveTracking is false) continue;
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
				Humidity = currentData.Relative_humidity_2m ?? -1,
				Temperature = currentData.Temperature_2m ?? -1000,
				Windspeed = currentData.Wind_speed_10m ?? -1,
				WeatherCode = (WeatherCode?)currentData.Weather_code ?? WeatherCode.UNKNOWN,
				Timestamp = DateTimeOffset.Now.ToLocalTime(),

				WindDirection = currentData.Wind_direction_10m ?? -1,
				Visibility = currentData.Visibility ?? -1,
				Showers = currentData.Showers ?? -1,
				Rain = currentData.Rain ?? -1,
				Snowfall = currentData.Snowfall ?? -1,
				PrecipitationProbability = currentData.Precipitation_probability ?? -1,
				SurfacePressure = currentData.Surface_pressure ?? -1,
				SoilMoisture_1_3cm = currentData.Soil_moisture_1_to_3cm ?? -1,
				SoilTemperature_6cm = currentData.Soil_temperature_6cm ?? -1,
				CloudCoverTotal = currentData.Cloud_cover ?? -1,

				Location = location,
				LocationId = location.Id
			});
			await Task.Delay(500); //wait half a second between API calls.
		}
		await dbContext.SaveChangesAsync();
	}
}

