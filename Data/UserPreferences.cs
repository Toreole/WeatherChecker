using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WeatherChecker.Data;

[PrimaryKey(nameof(DiscordSnowflake))]
[Index(nameof(DiscordSnowflake))]
public class UserPreferences(ulong snowflake) //ooouh new feature
{
	[Key]
	public ulong DiscordSnowflake { get; } = snowflake;

	public int DefaultLocationId { get; set; } = -1;
	public Location? DefaultLocation { get; set; }
}
