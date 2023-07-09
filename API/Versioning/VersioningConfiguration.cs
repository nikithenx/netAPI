
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace API.Versioning
{
    public static class VersioningConfiguration
    {
        public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader
                    .Combine(new UrlSegmentApiVersionReader(),
                             new HeaderApiVersionReader("x-api-version"),
                             new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureOptions<SwaggerConfiguration>();

            return services;
        }
    }
}