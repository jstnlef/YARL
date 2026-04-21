namespace YARL.Leaderboards;

public sealed record SongStatusResponse(
    bool IsOfficial,
    string? SourceFamily,
    bool SubmissionAllowed,
    string? ReasonCode);
