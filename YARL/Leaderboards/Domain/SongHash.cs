using System.Text.RegularExpressions;

namespace YARL.Leaderboards.Domain;

public readonly partial record struct SongHash(string Value)
{
    public override string ToString() => Value;

    public static bool TryParse(string? value, out SongHash songHash)
    {
        if (value is not null && SongHashRegex().IsMatch(value))
        {
            songHash = new SongHash(value.ToLowerInvariant());
            return true;
        }

        songHash = default;
        return false;
    }

    [GeneratedRegex("^[a-fA-F0-9]{40}$", RegexOptions.CultureInvariant)]
    private static partial Regex SongHashRegex();
}
