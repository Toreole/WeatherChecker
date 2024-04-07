using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherChecker.Data;

[PrimaryKey(nameof(Id))]
public class Forecast
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public DateTimeOffset Timestamp { get; set; }

	public int LocationId { get; set; }
	public Location Location { get; set; } = null!;

	public ICollection<ForecastWeatherData> ForecastWeatherData { get; set; } = [];
}
