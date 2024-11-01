namespace DailyExpenses.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationApi(this IServiceCollection services)
    {
        services.AddApplicationApiCORS();
        services.AddHttpContextAccessor();

        return services;
    }
}
