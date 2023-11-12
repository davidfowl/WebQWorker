internal static class AzureExtensions
{
    public static IResourceBuilder<T> UseAzureTracing<T>(this IResourceBuilder<T> builder) 
        where T : IResourceWithEnvironment
    {
        // Enable Activity Source tracing in the Azure SDK
        // Enable The Aspire Azure Service bus component's tracing
        return builder.WithEnvironment("AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE", "true")
                      .WithEnvironment("Aspire__Azure__Messaging__ServiceBus__Tracing", "true");
    }
}
