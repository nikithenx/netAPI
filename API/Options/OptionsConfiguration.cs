
namespace API.Options
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection ConfigureOptionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(DatabaseConnectionOptions.ConnectionStrings));
            return services;
        }
    }
}