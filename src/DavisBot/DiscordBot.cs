using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordAutoChannel.Configuration;

namespace DiscordAutoChannel
{
    public class DiscordBot
    {
        private readonly DiscordSocketClient _discord;
        private readonly BaseConfiguration _config;

        private bool _started;

        public DiscordBot(DiscordSocketClient discord,
                          BaseConfiguration config)
        {
            _discord = discord;
            _config = config;
        }

        public async Task Run()
        {
            await _discord.LoginAsync(TokenType.Bot,_config.BotApiKey);
            await _discord.StartAsync();
            
            _discord.Ready += async () =>
            {
                Console.WriteLine("Connected to discord!");

                await _discord.SetGameAsync("Telling you 'Fuck Davis' since 2020");
                
                if (!_started)
                {
                    OnStart();
                    _started = true;
                }
            };
            
            _discord.JoinedGuild += guild =>
            {
                Console.WriteLine($"Joined a new guild: '{guild.Name}'!");

                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private void OnStart()
        {
            var fDavis = "Fuck Davis.";
            
            _discord.MessageReceived += async message =>
            {
                if (message.Author.Id == _discord.CurrentUser.Id)
                {
                    return;
                }

                if(message.Channel is SocketTextChannel textChannel)
                {
                    if (IsDavis(message))
                    {
                        await textChannel.SendMessageAsync(fDavis);
                    }
                }

                if (message.Channel is SocketDMChannel dmChannel)
                {
                    if (IsDavis(message))
                    {
                        await dmChannel.SendMessageAsync(fDavis);
                    }
                }
            };
        }

        private bool IsDavis(SocketMessage message) => message.Content.ToLower().Contains("davis");
    }
}