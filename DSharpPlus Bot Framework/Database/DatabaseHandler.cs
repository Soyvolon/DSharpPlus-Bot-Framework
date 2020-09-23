using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Npgsql;
using DSharpPlus_Bot_Framework.Structures;
using DSharpPlus_Bot_Framework;
using System.Collections.Concurrent;

namespace PrintnecdoteBot.Database
{
    public static class DatabaseHandler
    {
        public static DatabaseConfig Database { get { return Program.Bot.Database; } }
        #region General

        #region Guild Prefix Manegement
        public static async Task<ConcurrentDictionary<ulong, GuildConfig>> GetGuildConfigsAsync()
        {
            var guildConfigs = new ConcurrentDictionary<ulong, GuildConfig>();

            using (NpgsqlConnection conn = new NpgsqlConnection(Database.GetConnectionString()))
            {
                await conn.OpenAsync().ConfigureAwait(false);

                using (var r = await new NpgsqlCommand($@"SELECT * FROM ""{Database.GeneralDb}"".""GeneralConfigs""", conn).ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await r.ReadAsync().ConfigureAwait(false))
                    {
                        GuildConfig gc = new GuildConfig(Convert.ToUInt64(r.GetInt64(0)));

                        try
                        {
                            gc.Prefix = r.GetString(1);
                        }
                        catch
                        {
                            gc.Prefix = Program.Bot.Config.Prefix;
                        }

                        guildConfigs.TryAdd(gc.GuildId, gc);
                    }
                }

                conn.Close();
            }

            return guildConfigs;
        }

        public static async Task<GuildConfig> GetSingleGuildConfigAsync(ulong GuildId)
        {
            using NpgsqlConnection server = new NpgsqlConnection(Database.GetConnectionString());
            await server.OpenAsync().ConfigureAwait(false);

            using var result = await new NpgsqlCommand($@"SELECT * FROM ""{Database.GeneralDb}"".""GeneralConfigs"" WHERE ""GuildId"" = {GuildId}", server).ExecuteReaderAsync().ConfigureAwait(false);

            if (result.HasRows)
            {
                while (await result.ReadAsync().ConfigureAwait(false))
                {
                    var config = new GuildConfig
                    {
                        GuildId = Convert.ToUInt64(result.GetString(0)),
                        Prefix = result.GetString(1)
                    };

                    return config;
                }
            }

            return null;
        }

        public static async Task<string> GetGuildPrefixAsync(ulong GuildId)
        {
            using NpgsqlConnection server = new NpgsqlConnection(Database.GetConnectionString());
            await server.OpenAsync().ConfigureAwait(false);

            var result = await new NpgsqlCommand($@"SELECT COUNT(Prefix) FROM ""{Database.GeneralDb}"".""GeneralConfigs"" WHERE ""GuildId"" = {Convert.ToInt64(GuildId)}", server).ExecuteScalarAsync().ConfigureAwait(false);

            if (result is null)
                return "pb!";

            if ((long)result > 0) //Check if Prefix is even set, if not ignore.
            {
                return new NpgsqlCommand($@"SELECT Prefix FROM ""{Database.GeneralDb}"".""GeneralConfigs"" WHERE ""GuildId"" = {Convert.ToInt64(GuildId)}", server).ExecuteScalarAsync().ConfigureAwait(false).ToString(); //Get guild Prefix from DB.
            }

            return "pb!";
        }

        public static async Task UpdateGuildConfigDatabaseAsync(GuildConfig config)
        {
            using NpgsqlConnection server = new NpgsqlConnection(Database.GetConnectionString());
            await server.OpenAsync().ConfigureAwait(false);

            using var cmd = new NpgsqlCommand($@"UPDATE ""{Database.GeneralDb}"".""GeneralConfigs"" SET ""Prefix"" = '{config.Prefix}' WHERE ""GuildId"" = {config.GuildId}", server);
            if (await cmd.ExecuteNonQueryAsync().ConfigureAwait(false) <= 0)
            {
                cmd.CommandText = $@"INSERT INTO ""{Database.GeneralDb}"".""GeneralConfigs"" VALUES ('{config.GuildId}','{config.Prefix}')";
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
        #endregion

        #endregion
    }
}
