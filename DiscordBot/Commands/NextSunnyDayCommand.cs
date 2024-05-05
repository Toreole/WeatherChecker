using Discord;
using Discord.Interactions;

namespace WeatherChecker.DiscordBot.Commands;

public class NextSunnyDayCommand : InteractionModuleBase<SocketInteractionContext>
{

	[SlashCommand("sun", "Tells you when the next sunny day is.")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task NextSunnyDayAsync()
	{
		Console.WriteLine("Sun Command");
		await RespondAsync("hello there", ephemeral: true);
	}
}
