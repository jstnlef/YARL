namespace YARL.Features.Leaderboards.Contracts;

public sealed record ApiEnvelope<T>(bool Success, T Data)
{
    public static ApiEnvelope<T> Ok(T data) => new(true, data);
}
