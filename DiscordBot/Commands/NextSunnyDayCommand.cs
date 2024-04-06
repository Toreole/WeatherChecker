using Discord;
using Discord.Interactions;

namespace WeatherChecker.DiscordBot.Commands;

public class NextSunnyDayCommand : InteractionModuleBase<SocketInteractionContext>
{
	private bool IsDm => Context.Channel.GetChannelType() == ChannelType.DM;

	[SlashCommand("sun", "Tells you when the next sunny day is.")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[IntegrationType(ApplicationIntegrationType.UserInstall, ApplicationIntegrationType.GuildInstall)]
	public async Task NextSunnyDayAsync()
	{
		Console.WriteLine("Command");
		await RespondAsync("hello there", ephemeral: !IsDm);
	}
}

public class HelloCommand : InteractionModuleBase<SocketInteractionContext>
{
	[SlashCommand("hello", "says hello")]
	[CommandContextType(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)]
	[UserCommand("hello")]
	[RequireUserPermission(ChannelPermission.SendMessages)]
	public async Task HelloAsync(IUser user)
	{
		await user.SendMessageAsync("hey there");
		await RespondAsync("done", ephemeral: true);
	}
}
