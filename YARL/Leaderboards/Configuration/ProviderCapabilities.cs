namespace YARL.Leaderboards.Configuration;

public sealed class ProviderCapabilities
{
    public bool SupportsOfficialSongs { get; set; } = true;

    public bool SupportsCustomSongs { get; set; }

    public bool SupportsBand { get; set; } = true;

    public bool SupportsReplayUpload { get; set; }

    public bool SupportsAccountRegistration { get; set; }
}
