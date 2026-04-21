using YARL.Infrastructure.Serialization;
using YARL.Leaderboards;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureYarlJson();
builder.Services.AddOpenApi();
builder.Services.AddLeaderboards(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapLeaderboards();

app.Run();

// ReSharper disable once ClassNeverInstantiated.Global
#pragma warning disable ASP0027
public partial class Program
#pragma warning restore ASP0027
{
}
