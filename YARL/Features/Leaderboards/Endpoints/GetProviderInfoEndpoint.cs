using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using YARL.Features.Leaderboards.Configuration;
using YARL.Features.Leaderboards.Contracts;

namespace YARL.Features.Leaderboards.Endpoints;

internal static class GetProviderInfoEndpoint
{
    public static RouteHandlerBuilder Map(RouteGroupBuilder group) =>
        group.MapGet("/info", Handle)
            .WithName("GetProviderInfo");

    private static Ok<ApiEnvelope<ProviderInfoResponse>> Handle(IOptions<ProviderMetadataOptions> options)
    {
        var response = new ProviderInfoResponse(
            options.Value.DisplayName,
            options.Value.ProviderType,
            options.Value.ApiVersion,
            options.Value.Capabilities);

        return TypedResults.Ok(ApiEnvelope<ProviderInfoResponse>.Ok(response));
    }
}
