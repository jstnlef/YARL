using System.Diagnostics.CodeAnalysis;

namespace YARL.Leaderboards;

public interface IOfficialSongCatalog
{
    bool TryGetSong(SongHash songHash, [NotNullWhen(true)] out OfficialSongCatalogEntry? song);
}
