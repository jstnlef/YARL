namespace YARL.Leaderboards;

public static class LeaderboardServiceCollectionExtensions
{
    public static IServiceCollection AddLeaderboardServices(this IServiceCollection services, IConfiguration configuration)
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
}
