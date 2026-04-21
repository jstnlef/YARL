using YARL.Features.Leaderboards.Configuration;
using YARL.Features.Leaderboards.Endpoints;
using YARL.Features.Leaderboards.Services;

namespace YARL.Features.Leaderboards;

public static class LeaderboardsModule
{
    public static IServiceCollection AddLeaderboards(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ProviderMetadataOptions>()
            .Bind(configuration.GetSection(ProviderMetadataOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.DisplayName) &&
                           !string.IsNullOrWhiteSpace(options.ProviderType) &&
                           !string.IsNullOrWhiteSpace(options.ApiVersion),
                "Provider metadata must include display name, provider type, and API version.")
            .ValidateOnStart();

        services.AddOptions<OfficialSongCatalogOptions>()
            .Bind(configuration.GetSection(OfficialSongCatalogOptions.SectionName))
            .ValidateOnStart();
        services.AddSingleton<IOfficialSongCatalog, InMemoryOfficialSongCatalog>();

        return services;
    }

    public static IEndpointRouteBuilder MapLeaderboards(this IEndpointRouteBuilder endpoints)
    {
        var providerGroup = endpoints.MapGroup("/provider");
        GetProviderInfoEndpoint.Map(providerGroup);

        var songGroup = endpoints.MapGroup("/songs");
        GetSongStatusEndpoint.Map(songGroup);

        return endpoints;
    }
}
