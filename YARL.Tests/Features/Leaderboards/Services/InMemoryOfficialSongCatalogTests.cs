using Microsoft.Extensions.Options;
using YARL.Features.Leaderboards.Configuration;
using YARL.Features.Leaderboards.Domain;
using YARL.Features.Leaderboards.Services;

namespace YARL.Tests.Features.Leaderboards.Services;

public class InMemoryOfficialSongCatalogTests
{
    [Fact]
    public void TryGetSong_ReturnsDeterministicResults_ForKnownAndUnknownHashes()
    {
        var options = Options.Create(new OfficialSongCatalogOptions
        {
            Songs =
            [
                new OfficialSongCatalogSongOptions
                {
                    SongHash = "0123456789abcdef0123456789abcdef01234567",
                    DisplayName = "Test Song",
                    SourceFamily = "yarg"
                }
            ]
        });

        var catalog = new InMemoryOfficialSongCatalog(options);

        var knownLookup = catalog.TryGetSong(
            new SongHash("0123456789abcdef0123456789abcdef01234567"),
            out var knownSong);
        var uppercaseLookup = catalog.TryGetSong(
            new SongHash("0123456789ABCDEF0123456789ABCDEF01234567"),
            out var uppercaseSong);
        var unknownLookup = catalog.TryGetSong(
            new SongHash("fedcba9876543210fedcba9876543210fedcba98"),
            out var unknownSong);

        Assert.True(knownLookup);
        Assert.Equal("yarg", knownSong?.SourceFamily);
        Assert.True(uppercaseLookup);
        Assert.Equal(knownSong, uppercaseSong);
        Assert.False(unknownLookup);
        Assert.Null(unknownSong);
    }
}
