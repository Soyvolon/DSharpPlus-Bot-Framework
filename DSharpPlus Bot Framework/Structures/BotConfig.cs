using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DSharpPlus_Bot_Framework.Structures
{
    public struct BotConfig
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("shards")]
        public int Shards { get; set; }

        [JsonProperty("superadmins")]
        public ulong[] Admins { get; set; }
    }
}
