using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using Npgsql;

namespace DSharpPlus_Bot_Framework.Structures
{
    public class DatabaseConfig
    {
        [JsonProperty]
        public string GeneralDb { get; private set; }

        [JsonProperty]
        private string database;
        [JsonProperty]
        private string hostname;
        [JsonProperty]
        private string username;
        [JsonProperty]
        private string password;

        [JsonConstructor]
        public DatabaseConfig(string db, string host, string user, string pass, string generalName)
        {
            database = db;
            hostname = host;
            username = user;
            password = pass;
            GeneralDb = generalName;
        }

        // Use this for connecting to a database.
        public string GetConnectionString()
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = hostname,
                Username = username,
                Password = password,
                Database = database
            }.ConnectionString;
        }
    }
}
