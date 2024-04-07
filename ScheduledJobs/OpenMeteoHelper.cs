using OpenMeteo.Options;

namespace WeatherChecker.ScheduledJobs;

internal static class OpenMeteoHelper
{
	static WeatherForecastOptions? dailyForecastOptions = null;
	static WeatherForecastOptions? currentWeatherOptions = null;

	internal static WeatherForecastOptions CurrentWeatherOptions
	{
		get
		{
			if (currentWeatherOptions == null)
			{
				currentWeatherOptions = new WeatherForecastOptions(
					latitude: WeatherCheckerService.config.latitude,
					longitude: WeatherCheckerService.config.longitude
				);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.weather_code);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.temperature_2m);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.wind_speed_10m);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.relative_humidity_2m);

				currentWeatherOptions.Current.Add(CurrentOptionsParameter.surface_pressure);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.wind_direction_10m);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.rain);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.showers);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.snowfall);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.cloud_cover);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.wind_gusts_10m);
				// this is allowed via the API
				currentWeatherOptions.Current.Add(HourlyOptionsParameter.visibility);
				currentWeatherOptions.Current.Add(HourlyOptionsParameter.precipitation_probability);
				currentWeatherOptions.Current.Add(HourlyOptionsParameter.soil_temperature_6cm);
				currentWeatherOptions.Current.Add(HourlyOptionsParameter.soil_moisture_1_to_3cm);
			}
			return currentWeatherOptions;
		}
	}

	internal static WeatherForecastOptions DailyForecastOptions
	{
		get
		{
			if (dailyForecastOptions == null)
			{
				dailyForecastOptions = new WeatherForecastOptions()
				{ 
					Temperature_Unit = TemperatureUnitType.celsius,
					Latitude = WeatherCheckerService.config.latitude,
					Longitude = WeatherCheckerService.config.longitude
				};
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.weather_code);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.temperature_2m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.wind_speed_10m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.relative_humidity_2m);

				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.surface_pressure);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.wind_direction_10m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.rain);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.showers);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.snowfall);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.cloud_cover);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.wind_gusts_10m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.soil_moisture_1_to_3cm);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.soil_temperature_6cm);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.visibility);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.precipitation_probability);
			}
			dailyForecastOptions.Start_date = DateTimeOffset.Now
				.AddDays(1)
				.ToString("yyyy-MM-dd");
			dailyForecastOptions.End_date = DateTimeOffset.Now
				.AddDays(WeatherCheckerService.config.forecastDays)
				.ToString("yyyy-MM-dd");
			return dailyForecastOptions;
		}
	}

}
