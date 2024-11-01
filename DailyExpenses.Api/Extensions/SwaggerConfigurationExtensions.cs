namespace DailyExpenses.Api.Extensions;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using DailyExpenses.Api.Utils.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

public static class SwaggerConfigurationExtensions
{
    public static IServiceCollection AddApplicationSwagger(this IServiceCollection services)
    {
        services
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DailyExpenses.Api",
                    Description = "An ASP.NET Core Web API for managing DailyExpenses.Api items",
                    TermsOfService = new Uri("http://localhost:5175/")
                });

                options.DescribeAllParametersInCamelCase();
                options.SupportNonNullableReferenceTypes();
                options.UseAllOfToExtendReferenceSchemas();
                options.UseAllOfForInheritance();
                options.SchemaFilter<EnumSchemaFilter>();
                options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
                options.CustomOperationIds(e =>
                {
                    var opId = e.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null;

                    return opId;
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        return services;
    }
}
