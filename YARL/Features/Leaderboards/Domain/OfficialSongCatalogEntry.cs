namespace YARL.Features.Leaderboards.Domain;

public sealed record OfficialSongCatalogEntry(
    SongHash SongHash,
    string? DisplayName,
    string SourceFamily);
