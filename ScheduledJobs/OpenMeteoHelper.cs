using OpenMeteo;

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
					latitude: 0,
					longitude: 0
				);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.weathercode);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.temperature_2m);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.windspeed_10m);
				currentWeatherOptions.Current.Add(CurrentOptionsParameter.relativehumidity_2m);
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
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.weathercode);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.temperature_2m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.windspeed_10m);
				dailyForecastOptions.Hourly.Add(HourlyOptionsParameter.relativehumidity_2m);
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
