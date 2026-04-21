using YARL.Features.Leaderboards;

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

public partial class Program
{
}
