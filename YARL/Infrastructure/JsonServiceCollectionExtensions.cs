using YARL.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class JsonServiceCollectionExtensions
{
    public static IServiceCollection ConfigureYarlJson(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });

        return services;
    }
}
