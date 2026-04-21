using YARL.Features.Leaderboards.Configuration;

namespace YARL.Features.Leaderboards.Contracts;

public sealed record ProviderInfoResponse(
    string DisplayName,
    string ProviderType,
    string ApiVersion,
    ProviderCapabilities Capabilities);
