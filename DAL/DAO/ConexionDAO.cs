using System;
using Microsoft.Data.SqlClient;

namespace DAL.DAO
{
    public static class ConexionDAO
    {
        private static string ArmarCadenaConexion()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB", 
                InitialCatalog = "PracticaCapas",
                IntegratedSecurity = true,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }

        public static string ConnectionString { get; } = ArmarCadenaConexion();
    }
}
