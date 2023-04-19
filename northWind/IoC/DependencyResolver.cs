using northWind.Domain.Repositorys;
using northWind.Infrastructure.Repositorys;
using northWind.Infrastructure.SqlServer;

namespace northWind.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.ConfigurarPersistenciaComSqlServer();
            services.AddScoped<INorthWindRepository, NorthWindRepository>();
        }
    }
}
