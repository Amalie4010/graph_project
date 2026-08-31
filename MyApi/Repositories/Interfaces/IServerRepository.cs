namespace MyApi.Repositories {
    internal interface IServerRepository {
        List<Server> GetAddresses();
        Server GetInfo(int id);
        string Add(string address, int playerMax);
        string UpdateInfo(int serverId, int max);
        string Delete(int serverId);
    }
}