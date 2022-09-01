using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurandoSqlServer.SqlServer
{
    public class ConexaoSqlServer : IOpcoes
    {
        public void Executar()
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-UA5T9LL\\SQLEXPRESS";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "104059";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Conectando-se ao SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Feito.");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Tudo feito. Pressione tecla 0 para finalizar ou selecione outra opção ...");

        }
    }
}
