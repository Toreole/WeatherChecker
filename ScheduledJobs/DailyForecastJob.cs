using OpenMeteo;
using Quartz;
using WeatherChecker.Data;
using WeatherChecker.ScheduledJobs;

namespace WeatherChecker.Jobs;

public class DailyForecastJob : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		using WeatherDBContext dbContext = new();
		OpenMeteoClient client = new();

		var forecastOptions = OpenMeteoHelper.DailyForecastOptions;
		var locations = dbContext.Set<Location>();

		foreach (var location in locations)
		{
			forecastOptions.Latitude = location.Latitude;
			forecastOptions.Longitude = location.Longitude;
			var result = await client.QueryAsync(forecastOptions);
			var hourlyData = result?.Hourly;
			if (hourlyData == null)
			{
				//Log that it has failed.
				return;
			}
			var now = DateTimeOffset.Now;
			for (int i = 0; i < hourlyData.Time?.Length; i++)
			{
				var forecast = new ForecastWeatherData()
				{
					Humidity = hourlyData.Relativehumidity_2m?[i] ?? -1,
					Temperature = hourlyData.Temperature_2m?[i] ?? -1000,
					Windspeed = hourlyData.Windspeed_10m?[i] ?? -1,
					WeatherCode = (WeatherCode?)hourlyData.Weathercode?[i] ?? WeatherCode.UNKNOWN,
					ForecastTimestamp = now.ToLocalTime(),
					PredictionTimestamp = DateTimeOffset.Parse(hourlyData.Time[i]).ToLocalTime(),

					WindDirection = hourlyData.Winddirection_10m?[i] ?? -1,
					Visibility = hourlyData.Visibility?[i] ?? -1,
					Showers = hourlyData.Showers?[i] ?? -1,
					Rain = hourlyData.Rain?[i] ?? -1,
					Snowfall = hourlyData.Snowfall?[i] ?? -1,
					PrecipitationProbability = hourlyData.Precipitation_probability?[i] ?? -1,
					SurfacePressure = hourlyData.Surface_pressure?[i] ?? -1,
					SoilMoisture_1_3cm = hourlyData.Soil_moisture_1_3cm?[i] ?? -1,
					SoilTemperature_6cm = hourlyData.Soil_temperature_6cm?[i] ?? -1,
					CloudCoverTotal = hourlyData.Cloudcover?[i] ?? -1,

					Location = location,
					LocationId = location.Id
				};
				dbContext.Forecasts.Add(forecast);
			}
			await dbContext.SaveChangesAsync();
			await Task.Delay(500);
		}

	}
}
