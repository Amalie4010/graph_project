using MyApi.Repositories;

public class ServerPollingService : BackgroundService {

    private readonly IServiceProvider _serviceProvider;

    public ServerPollingService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
        
        while (!cancellationToken.IsCancellationRequested) {
            try {
                using (var scope = _serviceProvider.CreateScope()) {
                    var serverRepo = scope.ServiceProvider.GetRequiredService<IServerRepository>();
                    var dataRepo = scope.ServiceProvider.GetRequiredService<IDataRepository>();

                    HttpService httpService = HttpService.GetSingleton();

                    List<Server> servers = serverRepo.GetAddresses();

                    foreach (Server server in servers) {
                        (int, int) res = await httpService.GetAsync(server.Address);
                        int online = res.Item1;
                        int max = res.Item2;

                        string addDataPoint = dataRepo.AddDataPoint(server.Id, online);
                        string updateServerInfo = serverRepo.UpdateInfo(server.Id, max);

                        Console.WriteLine(addDataPoint);
                        Console.WriteLine(updateServerInfo);
                    }

                    dataRepo.DeleteOldData();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }

            await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);
        }    
    }
}