using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
