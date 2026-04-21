namespace YARL.Infrastructure.Serialization;

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
