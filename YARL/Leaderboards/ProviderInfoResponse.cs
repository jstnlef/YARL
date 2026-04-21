namespace YARL.Leaderboards;

public sealed record ProviderInfoResponse(
    string DisplayName,
    string ProviderType,
    string ApiVersion,
    ProviderCapabilities Capabilities);
