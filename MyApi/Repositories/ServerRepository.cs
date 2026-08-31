using Microsoft.AspNetCore.Hosting.Server;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MyApi.Repositories {
    internal class ServerRepository : BaseRepository, IServerRepository {
        public List<Server> GetAddresses() {
            using MySqlConnection connection = GetConnection();
            connection.Open();

            List<Server> servers = new();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id, address FROM Server;";

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                Server serverAddress = new Server();
                serverAddress.Id = reader.GetInt32(0);
                serverAddress.Address = reader.GetString(1);

                servers.Add(serverAddress);
            }
            return servers;
        }

        public Server GetInfo(int id) {
            using MySqlConnection connection = GetConnection();
            connection.Open();

            Server serverInfo = new Server();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id, address, playerMax
                FROM Server
                WHERE id = @id
            "; 
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read()) {
                serverInfo.Id = reader.GetInt32(0);
                serverInfo.Address = reader.GetString(1);
                serverInfo.MaxPlayer = reader.GetInt32(2);
            }

            return serverInfo;
        }

        public string Add(string address, int playerMax) {
            try {
                using MySqlConnection connection = GetConnection();
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Server (address, playerMax)
                    VALUES (@address, @playerMax);
                "; 
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@playerMax", playerMax);
                command.ExecuteNonQuery();
                
                return "Server added.";
            }
            catch(MySqlException ex) {
                if (ex.Number == 1062) {
                    return "Server already exists.";
                }

                return $"Database error: {ex.Message}";
            }
            catch (Exception ex) {
                return ex.ToString();
            }
        }

        public string UpdateInfo(int serverId, int max) {
            try {
                using MySqlConnection connection = GetConnection();
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Server
                    SET playerMax = @playerMax
                    WHERE id = @serverId;
                ";
                command.Parameters.AddWithValue("@serverId", serverId);
                command.Parameters.AddWithValue("@playerMax", max);
                command.ExecuteNonQuery();

                return "Server info updated.";
            }
            catch (MySqlException ex) {
                return $"Database error: {ex.Message}";
            }
            catch (Exception ex) {
                return ex.ToString();
            }
        }

        public string Delete(int serverId) {
            try {
                using MySqlConnection connection = GetConnection();
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM Server
                    WHERE id = @serverId;
                "; 
                command.Parameters.AddWithValue("@serverId", serverId);
                command.ExecuteNonQuery();

                return "Server deleted.";
            }
            catch (MySqlException ex) {
                return $"Database error: {ex.Message}";
            }
            catch (Exception ex) {
                return ex.ToString();
            }
        }
    }
}