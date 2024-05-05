using Discord;
using Discord.Interactions;

namespace WeatherChecker.DiscordBot.Commands;

public class NextSunnyDayCommand : InteractionModuleBase<SocketInteractionContext>
{
	private bool IsDm => Context.Channel.GetChannelType() == ChannelType.DM;

	[SlashCommand("sun", "Tells you when the next sunny day is.")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall)]
	public async Task NextSunnyDayAsync()
	{
		Console.WriteLine("Command");
		await RespondAsync("hello there", ephemeral: !IsDm);
	}
}
