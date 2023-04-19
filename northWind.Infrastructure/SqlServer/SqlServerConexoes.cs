using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace northWind.Infrastructure.SqlServer
{
    internal class SqlServerConexoes : IDisposable
    {
        private readonly Dictionary<SqlServerAcessos, Lazy<SqlConnection>> _conexoesBanco = new ();
        public SqlServerConexoes(IConfiguration configuration)
        {
            _conexoesBanco.Add(SqlServerAcessos.northWind,new Lazy<SqlConnection> (() => 
            OpenConnection(CriarStringConexao(
                configuration["northWind_connection_string"],
                string.Empty,
                string.Empty
                ))));
        }

        public DbConnection this[SqlServerAcessos type]
        {
            get
            {
                if (_conexoesBanco.TryGetValue(type, out var con))
                    return con.Value;

                throw new NotSupportedException();
            }
        }

        private static string CriarStringConexao(string connectionString, string user, string password)
        {
            var sqlConnection = new SqlConnectionStringBuilder(connectionString)
            {
                ApplicationName = "northWind",
                MultipleActiveResultSets = true,
                TrustServerCertificate = true,
                Encrypt = false
            };

            if(sqlConnection.IntegratedSecurity)
                return sqlConnection.ToString();

            sqlConnection.UserID = user;
            sqlConnection.Password = password;
            return sqlConnection.ToString();
        }

        private static SqlConnection OpenConnection(string connectionString)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection; 
            }
            catch(Exception ex)
            {
                throw new Exception(connectionString, ex);
            }
            finally
            {
                stopWatch.Stop();
                Console.WriteLine($@"Conexão Aberta em: {stopWatch.ElapsedMilliseconds} ms");
            }
        }

        public void Dispose()
        {
            foreach (var connection in _conexoesBanco.Values)
            {
                if (!connection.IsValueCreated) continue;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                connection.Value.Close();
                connection.Value.Dispose();
                stopwatch.Stop();
                Console.WriteLine($@"Conexao fechada em {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
