using System.Collections.Generic;
using System.Linq;
using Env;

namespace DiscordAutoChannel.Configuration
{
    public class BaseConfiguration
    {
        private BaseConfiguration()
        {
            BotApiKey = string.Empty;
        }
        
        public string BotApiKey { get; set; }
    }
}