using System.ComponentModel.DataAnnotations;

namespace YARL.Leaderboards.Configuration;

public sealed class ProviderMetadataOptions
{
    public const string SectionName = "Leaderboards:Provider";

    [Required]
    public string DisplayName { get; set; } = "YARG Official";

    [Required]
    public string ProviderType { get; set; } = "official";

    [Required]
    public string ApiVersion { get; set; } = "v1";

    public ProviderCapabilities Capabilities { get; set; } = new();
}
