namespace MyApi.Repositories {
    internal interface IDataRepository {
        ServerData GetServerData(int id);
        string AddDataPoint(int serverId, int online);
        string DeleteOldData();
    }
}