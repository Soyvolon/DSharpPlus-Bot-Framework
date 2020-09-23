using System;
using System.Collections.Generic;
using System.Text;

using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace DSharpPlus_Bot_Framework.Commands
{
    public static class CommandUtils
    {
        public static DiscordEmbedBuilder ErrorBase(CommandContext ctx)
        {
            try
            {
                return new DiscordEmbedBuilder().WithColor(DiscordColor.Red).WithTimestamp(DateTime.Now).WithTitle($"{ctx.Command.Name} error").WithFooter($"{ctx.Prefix}{ctx.Command.Name}");
            }
            catch
            {
                return new DiscordEmbedBuilder();
            }
        }

        public static DiscordEmbedBuilder SuccessBase(CommandContext ctx)
        {
            return new DiscordEmbedBuilder().WithColor(DiscordColor.Green).WithTimestamp(DateTime.Now).WithFooter($"{ctx.Prefix}{ctx.Command.Name}");
        }
    }
}
