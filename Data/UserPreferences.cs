using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherChecker.Data;

[Index(nameof(DiscordSnowflake))]
public class UserPreferences
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public ulong DiscordSnowflake { get; set; }

	public int DefaultLocationId { get; set; } = -1;
	public Location? DefaultLocation { get; set; }

	public UserPreferences()
	{

	}
	public UserPreferences(ulong snowflake)
	{
		DiscordSnowflake = snowflake;
	}
}
