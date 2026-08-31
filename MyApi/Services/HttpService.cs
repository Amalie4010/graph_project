using System.Text.Json;

public class HttpService {

    // Atribute ----------------------------------------------------------
    private HttpClient sharedClient = new() {
        BaseAddress = new Uri("https://api.mcsrvstat.us/2/"),
    };

    // Singleton ----------------------------------------------------------
    private HttpService() { }
    private static readonly HttpService _singleton = new HttpService();
    public static HttpService GetSingleton() {
        return _singleton;
    }


    public async Task<(int, int)> GetAsync(string server) {
        sharedClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0"); // It's thinks im writing from a browser, when writing this
        using HttpResponseMessage response = await sharedClient.GetAsync(server);
        Console.WriteLine(response.EnsureSuccessStatusCode());
        string jsonResponse = await response.Content.ReadAsStringAsync();

        ServerStatus status = new ServerStatus();
        status = JsonSerializer.Deserialize<ServerStatus>(jsonResponse);

        if (status.online) {
            return (status.players["online"], status.players["max"]);
        }
        else {
            return (0, 0);
        }
        //Console.WriteLine($"{jsonResponse}\n");
    }
}
