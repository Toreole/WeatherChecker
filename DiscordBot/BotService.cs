using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using WeatherChecker.DiscordBot.Commands;

namespace WeatherChecker.DiscordBot
{
	public class BotService : BackgroundService
	{
		private readonly ILogger<BotService> _logger;
		private readonly IServiceProvider _serviceProvider;
		private readonly DiscordSocketClient _discordClient;
		private readonly InteractionService _interactionService;

		public BotService(ILogger<BotService> logger, IServiceProvider serviceProvider, DiscordSocketClient discordClient, InteractionService interactionService)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
			_discordClient = discordClient;
			_interactionService = interactionService;
		}

		public DateTimeOffset StartedAt { get; private set; }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Console.WriteLine("Running Discord Bot...");
			StartedAt = DateTimeOffset.Now;
			//_logger.LogInformation();

			await _interactionService.AddModuleAsync<NextSunnyDayCommand>(_serviceProvider).ConfigureAwait(false);
			var module = await _interactionService.AddModuleAsync<HelloCommand>(_serviceProvider).ConfigureAwait(false);
			foreach(var command in module.SlashCommands)
			{
				Console.WriteLine(string.Join(' ', command.ContextTypes.Select(x => x.ToString())));
				Console.WriteLine(command.CommandType);
			}

			_discordClient.Ready += OnReady;
			_discordClient.InteractionCreated += OnInteractionCreated;
			//_discordClient.SlashCommandExecuted += SlashCommandHandler;

			
			await _discordClient.LoginAsync(Discord.TokenType.Bot, ConfigurationManager.AppSettings.Get("discord_token")).ConfigureAwait(false);
			await _discordClient.StartAsync().ConfigureAwait(false);
		}

		private async Task SlashCommandHandler(SocketSlashCommand command)
		{
			await command.RespondAsync($"You executed {command.Data.Name}");
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			return base.StopAsync(cancellationToken);
		}

		public async Task OnInteractionCreated(SocketInteraction interaction)
		{
			var context = new SocketInteractionContext(_discordClient, interaction);
			IResult result = await _interactionService.ExecuteCommandAsync(context, _serviceProvider);
			if (!result.IsSuccess)
			{
				await interaction.RespondAsync("Something went wrong :(");
			}
		}

		private async Task OnReady()
		{
			Console.WriteLine("OnReady");

			//var command = new SlashCommandBuilder()
			//	.WithName("hello")
			//	.WithDescription("says hello")
			//	.WithIntegrationTypes(ApplicationIntegrationType.UserInstall)
			//	.WithContextTypes(InteractionContextType.BotDm, InteractionContextType.Guild, InteractionContextType.PrivateChannel);

			//var other = new SlashCommandBuilder()
			//	.WithName("sun")
			//	.WithDescription("uhm")
			//	.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
			//	.WithContextTypes(InteractionContextType.BotDm, InteractionContextType.Guild, InteractionContextType.PrivateChannel);

			//await _discordClient.CreateGlobalApplicationCommandAsync(command.Build());
			//await _discordClient.CreateGlobalApplicationCommandAsync(other.Build());
			await _discordClient.SetStatusAsync(Discord.UserStatus.Online);
			await _discordClient.SetGameAsync("Looking directly at the sun...");
			await _interactionService.RegisterCommandsGloballyAsync();
		}
	}
}
