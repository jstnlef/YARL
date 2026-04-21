using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YARL.Leaderboards.Contracts;
using YARL.Leaderboards.Domain;
using YARL.Leaderboards.Services;

namespace YARL.Leaderboards.Endpoints;

internal static class GetSongStatusEndpoint
{
    public static RouteHandlerBuilder Map(RouteGroupBuilder group) =>
        group.MapGet("/{songHash}/status", Handle)
            .WithName("GetSongStatus");

    private static Results<Ok<ApiEnvelope<SongStatusResponse>>, ValidationProblem> Handle(
        string songHash,
        IOfficialSongCatalog catalog)
    {
        if (!SongHash.TryParse(songHash, out var parsedSongHash))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                ["songHash"] = ["Song hash must be a 40-character SHA-1 hex string."]
            });
        }

        if (catalog.TryGetSong(parsedSongHash, out var song))
        {
            var knownSongResponse = new SongStatusResponse(
                IsOfficial: true,
                SourceFamily: song.SourceFamily,
                SubmissionAllowed: true,
                ReasonCode: null);

            return TypedResults.Ok(ApiEnvelope<SongStatusResponse>.Ok(knownSongResponse));
        }

        var unknownSongResponse = new SongStatusResponse(
            IsOfficial: false,
            SourceFamily: null,
            SubmissionAllowed: false,
            ReasonCode: "not_in_official_catalog");

        return TypedResults.Ok(ApiEnvelope<SongStatusResponse>.Ok(unknownSongResponse));
    }
}
