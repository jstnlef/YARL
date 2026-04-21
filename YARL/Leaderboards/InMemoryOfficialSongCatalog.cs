using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace YARL.Leaderboards;

public sealed class InMemoryOfficialSongCatalog : IOfficialSongCatalog
{
    private readonly IReadOnlyDictionary<string, OfficialSongCatalogEntry> _songs;

    public InMemoryOfficialSongCatalog(IOptions<OfficialSongCatalogOptions> options)
    {
        var songs = new Dictionary<string, OfficialSongCatalogEntry>(StringComparer.OrdinalIgnoreCase);

        foreach (var song in options.Value.Songs)
        {
            if (!SongHash.TryParse(song.SongHash, out var songHash))
            {
                throw new InvalidOperationException($"Configured official song hash '{song.SongHash}' is not a valid SHA-1 hash.");
            }

            if (string.IsNullOrWhiteSpace(song.SourceFamily))
            {
                throw new InvalidOperationException($"Configured official song '{song.SongHash}' is missing a source family.");
            }

            songs[songHash.Value] = new OfficialSongCatalogEntry(songHash, song.DisplayName, song.SourceFamily);
        }

        _songs = songs;
    }

    public bool TryGetSong(SongHash songHash, [NotNullWhen(true)] out OfficialSongCatalogEntry? song) =>
        _songs.TryGetValue(songHash.Value, out song);
}
