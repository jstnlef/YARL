namespace YARL.Features.Leaderboards.Configuration;

public sealed class OfficialSongCatalogOptions
{
    public const string SectionName = "Leaderboards:OfficialSongCatalog";

    public List<OfficialSongCatalogSongOptions> Songs { get; set; } = [];
}

public sealed class OfficialSongCatalogSongOptions
{
    public string SongHash { get; set; } = string.Empty;

    public string? DisplayName { get; set; }

    public string SourceFamily { get; set; } = string.Empty;
}
