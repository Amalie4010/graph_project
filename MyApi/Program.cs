using MyApi.Repositories;
using System.Diagnostics;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("http://localhost:5173")  // React port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHostedService<ServerPollingService>();

builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IDataRepository, DataRepository>();

WebApplication app = builder.Build();
app.UseCors();

// Get Addresses
app.MapGet("/server", (IServerRepository serverRepo) => {
    List<Server> addresses = serverRepo.GetAddresses();
    return Results.Ok(addresses);
});

// Get data for specific server, for graph
app.MapGet("/server/data/{serverid}", (IDataRepository dataRepo, int serverid) => {
    ServerData data = dataRepo.GetServerData(serverid);
    return data;
});

// Get info for specific server
app.MapGet("/server/{serverid}", async (IServerRepository serverRepo, int serverid) => {
    Server Info = serverRepo.GetInfo(serverid);
    return Info;
});

app.MapPost("/server", async (IServerRepository serverRepo, Server request) => {
    Server serverInfo = new();
    
    HttpService httpService = HttpService.GetSingleton();

    (int, int) tuble = await httpService.GetAsync(request.Address);
    int max = tuble.Item2;

    string result = serverRepo.Add(request.Address, max);

    return Results.Ok(result);
});

app.MapDelete("/server/{serverid}", (IServerRepository serverRepo, int serverid) => {
    string result = serverRepo.Delete(serverid);
    return Results.Ok(result);
});

app.Run();