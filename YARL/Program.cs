using YARL.Infrastructure;
using YARL.Leaderboards;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureYarlJson();
builder.Services.AddOpenApi();
builder.Services.AddLeaderboardServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapLeaderboardEndpoints();

app.Run();

public partial class Program
{
}
