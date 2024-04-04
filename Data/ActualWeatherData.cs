using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherChecker.Data;

[PrimaryKey(nameof(Id))]
[Index(nameof(Id), nameof(Timestamp))]
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
	public WeatherCode WeatherCode { get; set; }
}
