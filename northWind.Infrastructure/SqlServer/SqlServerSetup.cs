using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace northWind.Infrastructure.SqlServer
{
    public static class SqlServerSetup
    {
        public static void ConfigurarPersistenciaComSqlServer(this IServiceCollection services)
        {
            services.AddScoped<SqlServerConexoes>().AddScoped(s => (SqlConnection)s.GetService<SqlServerConexoes>()?[SqlServerAcessos.northWind]);

        }
    }
}
