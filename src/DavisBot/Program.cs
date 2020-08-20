using System;
using System.IO;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordAutoChannel.Configuration;
using Env;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordAutoChannel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = ConfigureServices(); 

            var discordBot = service.GetService<DiscordBot>();
            
            await Task.Run(async () => await discordBot.Run());
        }
        
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Add configuration
            var config = ConfigurationParser.Parse<BaseConfiguration>(Path.Combine(Environment.CurrentDirectory, "env"));
            services.AddSingleton(config);
            
            services.AddHttpClient();

            // Add basic services
            services.AddSingleton<DiscordBot>();
            services.AddSingleton<DiscordSocketClient>();

            return services.BuildServiceProvider();
        }
    }
}
