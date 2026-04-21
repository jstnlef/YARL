using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace YARL.Leaderboards;

public static class LeaderboardEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapLeaderboardEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var providerGroup = endpoints.MapGroup("/provider");
        providerGroup.MapGet("/info", GetProviderInfo)
            .WithName("GetProviderInfo");

        var songGroup = endpoints.MapGroup("/songs");
        songGroup.MapGet("/{songHash}/status", GetSongStatus)
            .WithName("GetSongStatus");

        return endpoints;
    }

    private static Ok<ApiEnvelope<ProviderInfoResponse>> GetProviderInfo(IOptions<ProviderMetadataOptions> options)
    {
        var response = new ProviderInfoResponse(
            options.Value.DisplayName,
            options.Value.ProviderType,
            options.Value.ApiVersion,
            options.Value.Capabilities);

        return TypedResults.Ok(ApiEnvelope<ProviderInfoResponse>.Ok(response));
    }

    private static Results<Ok<ApiEnvelope<SongStatusResponse>>, ValidationProblem> GetSongStatus(
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
