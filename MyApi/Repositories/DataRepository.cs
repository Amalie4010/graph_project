using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MyApi.Repositories {
    internal class DataRepository : BaseRepository, IDataRepository {
        public ServerData GetServerData(int id) {
            using MySqlConnection connection = GetConnection();
            connection.Open();

            ServerData serverData = new();
            List<int> sampleData = new();
            List<string> xnm = new();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT DATE_FORMAT(time + INTERVAL 2 HOUR, '%H:%i') AS danish_time, playerAmount 
                FROM Status
                WHERE serverId = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            while (reader.Read()) {
                xnm.Add(reader.GetString(0));
                sampleData.Add(reader.GetInt32(1));
            }

            serverData.SampleData = sampleData;
            serverData.Xnm = xnm;
            
            return serverData;
        }

        public string AddDataPoint(int serverId, int online) {
            try {
                using MySqlConnection connection = GetConnection();
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Status (serverId, playerAmount)
                    VALUES (@serverId, @playerAmount);
                "; 
                command.Parameters.AddWithValue("@serverId", serverId);
                command.Parameters.AddWithValue("@playerAmount", online);
                command.ExecuteNonQuery();
                
                return "Data point added.";
            }
            catch (MySqlException ex) {
                return $"Database error: {ex.Message}";
            }
            catch (Exception ex) {
                return ex.ToString();
            }
        }

        public string DeleteOldData() {
            try {
                using var connection = GetConnection();
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                    DELETE FROM Status 
                    WHERE time < NOW() - INTERVAL 12 HOUR;
                ";
                command.ExecuteNonQuery();
                
                return "Old data deleted.";
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