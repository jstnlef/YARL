using System.Diagnostics.CodeAnalysis;
using YARL.Leaderboards.Domain;

namespace YARL.Leaderboards.Services;

public interface IOfficialSongCatalog
{
    bool TryGetSong(SongHash songHash, [NotNullWhen(true)] out OfficialSongCatalogEntry? song);
}
