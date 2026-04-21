using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using YARL.Leaderboards.Contracts;

namespace YARL.Infrastructure.Serialization;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ApiEnvelope<ProviderInfoResponse>))]
[JsonSerializable(typeof(ApiEnvelope<SongStatusResponse>))]
[JsonSerializable(typeof(ValidationProblemDetails))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
