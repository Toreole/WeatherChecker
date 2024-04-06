using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherChecker.Data;

[PrimaryKey(nameof(Id))]
[Index(nameof(Id))]
[Index(nameof(Timestamp))]
public class ActualWeatherData
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	/// <summary>
	/// The timestamp of when this data was 'measured'.
	/// </summary>
	public DateTimeOffset Timestamp { get; set; }

	public float Temperature { get; set; }
	public float Humidity { get; set; }
	public float Windspeed { get; set; }
	public float WindDirection { get; set; }
	public WeatherCode WeatherCode { get; set; }
	public float CloudCoverTotal { get; set; }

	public float SurfacePressure { get; set; }

	public float Visibility { get; set; }

	public float PrecipitationProbability { get; set; }
	public float Rain { get; set; }
	public float Showers { get; set; }
	public float Snowfall { get; set; }

	public float SoilTemperature_6cm { get; set; }
	public float SoilMoisture_1_3cm { get; set; }

	public int LocationId { get; set; }
	public Location Location { get; set; } = null!;
}
