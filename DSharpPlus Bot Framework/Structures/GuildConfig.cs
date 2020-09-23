using System;
using System.Collections.Generic;
using System.Text;

namespace DSharpPlus_Bot_Framework.Structures
{
    public class GuildConfig
    {
        public ulong GuildId { get; set; }

        public string Prefix { get; set; }

        public GuildConfig() { }

        public GuildConfig(ulong gid)
        {
            GuildId = gid;
        }

        public GuildConfig(ulong gid, string p)
        {
            GuildId = gid;

            Prefix = p;
        }

    }
}
