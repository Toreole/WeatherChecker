using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WeatherChecker.Data;

[PrimaryKey(nameof(Id))]
[Index(nameof(Id), nameof(PredictionTimestamp), nameof(ForecastTimestamp))]
public class ForecastWeatherData
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	/// <summary>
	/// The timestamp for the "future" point in time this forecast is referring to.
	/// </summary>
	public DateTimeOffset PredictionTimestamp { get; set; }

	/// <summary>
	/// The timestamp of when the forecast was made.
	/// </summary>
	public DateTimeOffset ForecastTimestamp { get; set; }

	public float Temperature { get; set; }
	public float Humidity { get; set; }
	public float Windspeed { get; set; }
	public WeatherCode WeatherCode { get; set; }
}
