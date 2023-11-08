internal static class AzureExtensions
{
    public static IResourceBuilder<T> UseAzureTracing<T>(this IResourceBuilder<T> builder) 
        where T : IResourceWithEnvironment
    {
        return builder.WithEnvironment("AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE", "true")
                      .WithEnvironment("Aspire__Azure__Messaging__ServiceBus__Tracing", "true");
    }
}
