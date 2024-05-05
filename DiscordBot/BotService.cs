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

			_discordClient.Ready += OnReady;
			_discordClient.InteractionCreated += OnInteractionCreated;
			
			await _discordClient.LoginAsync(Discord.TokenType.Bot, ConfigurationManager.AppSettings.Get("discord_token")).ConfigureAwait(false);
			await _discordClient.StartAsync().ConfigureAwait(false);
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
			await _discordClient.SetStatusAsync(Discord.UserStatus.Online);
			await _discordClient.SetGameAsync("Looking directly at the sun...");
			await _interactionService.RegisterCommandsGloballyAsync();
		}
	}
}
