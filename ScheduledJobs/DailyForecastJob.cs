using OpenMeteo;
using Quartz;
using WeatherChecker.Data;

namespace WeatherChecker.ScheduledJobs;

public class DailyForecastJob : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		using WeatherDBContext dbContext = new();
		OpenMeteoClient client = new();

		var forecastOptions = OpenMeteoHelper.DailyForecastOptions;
		var locations = dbContext.Set<Location>().ToList();

		foreach (var location in locations)
		{
			if (location.ActiveTracking is false) continue;
			forecastOptions.Latitude = location.Latitude;
			forecastOptions.Longitude = location.Longitude;
			var result = await client.QueryAsync(forecastOptions);
			var hourlyData = result?.Hourly;
			if (hourlyData == null)
			{
				//Log that it has failed.
				return;
			}
			//create Forecast
			Forecast forecast = new()
			{
				Timestamp = DateTime.Now,
				Location = location,
				LocationId = location.Id,
			};
			dbContext.Forecasts.Add(forecast);
			await dbContext.SaveChangesAsync();

			for (int i = 0; i < hourlyData.Time?.Length; i++)
			{
				var forecastData = new ForecastWeatherData()
				{
					Humidity = hourlyData.Relative_humidity_2m?[i] ?? -1,
					Temperature = hourlyData.Temperature_2m?[i] ?? -1000,
					Windspeed = hourlyData.Wind_speed_10m?[i] ?? -1,
					WeatherCode = (WeatherCode?)hourlyData.Weather_code?[i] ?? WeatherCode.UNKNOWN,
					PredictionTimestamp = DateTimeOffset.Parse(hourlyData.Time[i]).ToLocalTime(),

					WindDirection = hourlyData.Wind_direction_10m?[i] ?? -1,
					Visibility = hourlyData.Visibility?[i] ?? -1,
					Showers = hourlyData.Showers?[i] ?? -1,
					Rain = hourlyData.Rain?[i] ?? -1,
					Snowfall = hourlyData.Snowfall?[i] ?? -1,
					PrecipitationProbability = hourlyData.Precipitation_probability?[i] ?? -1,
					SurfacePressure = hourlyData.Surface_pressure?[i] ?? -1,
					SoilMoisture_1_3cm = hourlyData.Soil_moisture_1_to_3cm?[i] ?? -1,
					SoilTemperature_6cm = hourlyData.Soil_temperature_6cm?[i] ?? -1,
					CloudCoverTotal = hourlyData.Cloud_cover?[i] ?? -1,

					Forecast = forecast,
					ForecastId = forecast.Id
				};
				dbContext.ForecastWeatherData.Add(forecastData);
			}
			await dbContext.SaveChangesAsync();
			await Task.Delay(500);
		}

	}
}
