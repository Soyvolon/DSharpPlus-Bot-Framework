using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DSharpPlus_Bot_Framework.Commands.Utility
{
    public class Help : BaseCommandModule
    {
        [Command("help")]
        public async Task HelpAsync(CommandContext ctx, string command)
        {
            DiscordEmbedBuilder b = null;

            switch(command.ToLower().Trim())
            {
                case "help":
                    b = new DiscordEmbedBuilder();
                    b.WithTitle($"{ctx.Prefix}help");
                    b.WithDescription($"Displays information about the specified command, or information about all commands!");
                    b.AddField("Format: ", $"{ctx.Prefix}help [command name]");
                    b.AddField("Format: ", $"{ctx.Prefix}help");
                    break;
            }

            if (b is null)
                await HelpAsync(ctx).ConfigureAwait(false);
            else
                await ctx.RespondAsync(embed: b.Build()).ConfigureAwait(false);
        }

        [Command("help")]
        public async Task HelpAsync(CommandContext ctx)
        {
            DiscordEmbedBuilder b = new DiscordEmbedBuilder();
            b.WithTitle("Bot Commands:").WithColor(DiscordColor.White).WithDescription("Specify a command below with {prefix}[command]");
            b.AddField("Utility Commands: ", $"");
            await ctx.RespondAsync(embed: b.Build()).ConfigureAwait(false);
        }
    }
}
