using YARL.Leaderboards.Configuration;

namespace YARL.Leaderboards.Contracts;

public sealed record ProviderInfoResponse(
    string DisplayName,
    string ProviderType,
    string ApiVersion,
    ProviderCapabilities Capabilities);
