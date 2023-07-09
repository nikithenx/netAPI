using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Versioning
{
    public class SwaggerConfiguration : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfiguration(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                Title = ".NET Core Web API",
                Version = desc.ApiVersion.ToString(),
                Description = "This is my Web API"
            };

            if (desc.IsDeprecated)
            {
                info.Description += " This API version is deprecated";
            }

            return info;
        }
    }
}