using System.Diagnostics.CodeAnalysis;
using YARL.Features.Leaderboards.Domain;

namespace YARL.Features.Leaderboards.Services;

public interface IOfficialSongCatalog
{
    bool TryGetSong(SongHash songHash, [NotNullWhen(true)] out OfficialSongCatalogEntry? song);
}
