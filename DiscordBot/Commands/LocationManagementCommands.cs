using Discord;
using Discord.Commands;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using WeatherChecker.Data;

namespace WeatherChecker.DiscordBot.Commands;

public class LocationManagementCommands : InteractionModuleBase<SocketInteractionContext>
{

	[SlashCommand("setlocation", "Sets your default location when invoking commands.")]
	[Alias("sl")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task SetUserLocation(string locationName)
	{
		Console.WriteLine("SetLocation Command");
		using WeatherDBContext dbContext = new();
		var userPref = dbContext.UserPreferences.FirstOrDefault(x => x.DiscordSnowflake == Context.User.Id);
		if (userPref is null)
		{
			Console.WriteLine($"Creating new UserPref for {Context.User.GlobalName}");
			userPref = new(Context.User.Id);
			dbContext.UserPreferences.Add(userPref);
		}
		var location = dbContext.Locations.FirstOrDefault(x => EF.Functions.Like(x.Name, $"{locationName}%"));
		if (location is null)
		{
			Console.WriteLine($"Could not find location like {locationName}");
			await RespondAsync($"Could not find location with name like '{locationName}'.", ephemeral: true);
		}
		else
		{
			Console.WriteLine("Set.");
			userPref.DefaultLocation = location;
			userPref.DefaultLocationId = location.Id;
			dbContext.UserPreferences.Update(userPref);
			await RespondAsync($"Set your location to {location.Name} @ {location.Latitude:00.000}° lat, {location.Latitude:00.000}° lon", ephemeral: true);
		}
		dbContext.SaveChanges();
	}

	[SlashCommand("addlocation", "Creates a new location to track weather data for.")]
	[Alias("addl")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task AddLocation(string locationName, float latitude, float longitude)
	{
		Console.WriteLine("AddLocation Command");
		using WeatherDBContext dbContext = new();
		var location = dbContext.Locations.FirstOrDefault(x => EF.Functions.Like(x.Name, $"{locationName}%"));
		if (location is not null)
		{
			await RespondAsync("Location with name already exists.", ephemeral: true);
		}
		else
		{
			location = new() { Name = locationName, Latitude = latitude, Longitude = longitude };
			dbContext.Locations.Add(location);
			dbContext.SaveChanges();
			await RespondAsync("Created location.", ephemeral: true);
		}
	}

	[SlashCommand("setlocationactive", "Sets whether the weather data for this location should be gathered.")]
	[Alias("sla")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task UpdateLocationStatus(string locationName, bool trackWeather)
	{
		Console.WriteLine("UpdateLocation Command");
		using WeatherDBContext dbContext = new();
		var location = dbContext.Locations.FirstOrDefault(x => x.Name == locationName);
		if (location is null)
		{
			Console.WriteLine($"Could not find location with name '{locationName}'.");
			await RespondAsync($"Could not find location with name '{locationName}'.", ephemeral: true);
		} 
		else
		{
			location.ActiveTracking = trackWeather;
			dbContext.Update(location);
			dbContext.SaveChanges();
			Console.WriteLine($"Updated location {locationName} with id {location.Id}");
			await RespondAsync("Updated.", ephemeral: true);
		}
	}

	[SlashCommand("listlocations", "Lists all tracked locations.")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task ListLocations()
	{
		using WeatherDBContext dBContext = new();
		var locations = dBContext.Locations.Where(x => x.ActiveTracking == true).Select(x => x.Name).ToList();
		var text = string.Join("\n", locations);
		await RespondAsync(text, ephemeral: true);
	}
}
