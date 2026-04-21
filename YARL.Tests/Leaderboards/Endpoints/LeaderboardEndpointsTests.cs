using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using YARL.Leaderboards.Contracts;
using YARL.Tests.Leaderboards;

namespace YARL.Tests.Leaderboards.Endpoints;

public class LeaderboardEndpointsTests(TestLeaderboardApiFactory factory) : IClassFixture<TestLeaderboardApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetProviderInfo_ReturnsExpectedMetadataEnvelope()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var response = await _client.GetAsync("/provider/info", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ApiEnvelope<ProviderInfoResponse>>(cancellationToken: cancellationToken);
        Assert.NotNull(payload);
        Assert.True(payload.Success);
        Assert.Equal("Test Official Provider", payload.Data.DisplayName);
        Assert.Equal("official", payload.Data.ProviderType);
        Assert.Equal("test-v1", payload.Data.ApiVersion);
        Assert.True(payload.Data.Capabilities.SupportsOfficialSongs);
        Assert.False(payload.Data.Capabilities.SupportsCustomSongs);
        Assert.True(payload.Data.Capabilities.SupportsBand);
    }

    [Fact]
    public async Task GetSongStatus_ForKnownSong_ReturnsOfficialSubmissionAllowed()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var response = await _client.GetAsync($"/songs/{TestLeaderboardApiFactory.KnownSongHash}/status", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ApiEnvelope<SongStatusResponse>>(cancellationToken: cancellationToken);

        Assert.NotNull(payload);
        Assert.True(payload.Success);
        Assert.True(payload.Data.IsOfficial);
        Assert.Equal("yarg", payload.Data.SourceFamily);
        Assert.True(payload.Data.SubmissionAllowed);
        Assert.Null(payload.Data.ReasonCode);
    }

    [Fact]
    public async Task GetSongStatus_ForUnknownSong_ReturnsNotInOfficialCatalog()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var response = await _client.GetAsync($"/songs/{TestLeaderboardApiFactory.UnknownSongHash}/status", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ApiEnvelope<SongStatusResponse>>(cancellationToken: cancellationToken);

        Assert.NotNull(payload);
        Assert.True(payload.Success);
        Assert.False(payload.Data.IsOfficial);
        Assert.Null(payload.Data.SourceFamily);
        Assert.False(payload.Data.SubmissionAllowed);
        Assert.Equal("not_in_official_catalog", payload.Data.ReasonCode);
    }

    [Fact]
    public async Task GetSongStatus_ForMalformedHash_ReturnsValidationProblem()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var response = await _client.GetAsync("/songs/not-a-sha1/status", cancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(cancellationToken: cancellationToken);

        Assert.NotNull(payload);
        Assert.Contains("songHash", payload.Errors.Keys);
    }
}
