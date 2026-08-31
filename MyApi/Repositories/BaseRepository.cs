using MySqlConnector;

namespace MyApi.Repositories {
    internal abstract class BaseRepository {
        protected readonly string ConnectionString;

        protected BaseRepository() {
            ConnectionString = "Server=db;Port=3306;Database=minecraft;Uid=root;Pwd=;";
            //ConnectionString = "Server=127.0.0.1;Port=3306;Database=minecraft;Uid=root;Pwd=;";

        }

        protected MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }
    }
}