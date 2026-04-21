using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using YARL.Leaderboards.Configuration;

namespace YARL.Tests.Leaderboards;

public sealed class TestLeaderboardApiFactory : WebApplicationFactory<Program>
{
    public const string KnownSongHash = "0123456789abcdef0123456789abcdef01234567";
    public const string UnknownSongHash = "fedcba9876543210fedcba9876543210fedcba98";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            services.PostConfigure<ProviderMetadataOptions>(options =>
            {
                options.DisplayName = "Test Official Provider";
                options.ProviderType = "official";
                options.ApiVersion = "test-v1";
                options.Capabilities = new ProviderCapabilities
                {
                    SupportsOfficialSongs = true,
                    SupportsCustomSongs = false,
                    SupportsBand = true,
                    SupportsReplayUpload = false,
                    SupportsAccountRegistration = false
                };
            });

            services.PostConfigure<OfficialSongCatalogOptions>(options =>
            {
                options.Songs =
                [
                    new OfficialSongCatalogSongOptions
                    {
                        SongHash = KnownSongHash,
                        DisplayName = "Known Test Song",
                        SourceFamily = "yarg"
                    }
                ];
            });
        });
    }
}
