namespace YARL.Leaderboards;

public sealed record OfficialSongCatalogEntry(
    SongHash SongHash,
    string? DisplayName,
    string SourceFamily);
