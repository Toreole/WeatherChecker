using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WeatherChecker.Data;


[Index(nameof(Id))]
[Index(nameof(Name))]
public class Location
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Name { get; set; } = "";

	public float Latitude { get; set; }
	public float Longitude { get; set; }

	public bool ActiveTracking { get; set; } = false;
}
