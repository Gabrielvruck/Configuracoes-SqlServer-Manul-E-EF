using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurandoSqlServer.SqlServer
{
    public class ConexaoSqlServerCriacaoTabela : IOpcoes
    {
        public void Executar()
        {
            try
            {
                Console.WriteLine("Conecte-se ao SQL Server e demonstre as operações Criar, Ler, Atualizar e Excluir.");
                Console.WriteLine();

                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-UA5T9LL\\SQLEXPRESS";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "104059";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Conectando-se ao SQL Server ... ");
                Console.WriteLine();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Feito.");
                    //pular linha
                    Console.WriteLine();

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // Criar um banco de dados de exemplo
                    Console.Write("Descartando e criando banco de dados 'SimplesExemploDB' ... ");
                    Console.WriteLine();
                    String sql = "DROP DATABASE IF EXISTS [SimplesExemploDB]; CREATE DATABASE [SimplesExemploDB]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Feito.");
                        Console.WriteLine();
                    }

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // Crie uma tabela e insira alguns dados de exemplo
                    Console.Write("Criando tabela de amostra com dados, pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE SimplesExemploDB; ");
                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    //criando tabela
                    sb.Append("CREATE TABLE Funcionarios ( ");
                    sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" Nome NVARCHAR(50), ");
                    sb.Append(" Localizacao NVARCHAR(50) ");
                    sb.Append("); ");

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    //inserindo na tabela
                    sb.Append("INSERT INTO Funcionarios (Nome, Localizacao) VALUES ");
                    sb.Append("('Gabriel','Brasil'), ");
                    sb.Append("('Jair', 'Japão'), ");
                    sb.Append("('Caio', 'Austrália'); ");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Feito.");
                        Console.WriteLine();
                    }

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // INSERT demo
                    Console.Write("Inserindo uma nova linha na tabela, pressione qualquer tecla para continuar...");
                    Console.WriteLine();
                    Console.ReadKey(true);

                    sb.Clear();
                    sb.Append("INSERT Funcionarios (Nome, Localizacao) ");
                    sb.Append("VALUES (@Nome, @Localizacao);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", "Jake");
                        command.Parameters.AddWithValue("@Localizacao", "Estados Unidos");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " linha(s) inserida");
                    }

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // UPDATE demo
                    String userToUpdate = "Jair";
                    Console.Write("Atualizando 'Localicao' para o usuário '" + userToUpdate + "', pressione qualquer tecla para continuar...");
                    Console.WriteLine();
                    Console.ReadKey(true);

                    //pular linha
                    sb.Clear();
                    sb.Append("UPDATE Funcionarios SET Localizacao = 'Estados Unidos' WHERE Nome = @Nome");
                    Console.WriteLine();
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " linha(s) atualizada");
                    }

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // Deletar demo
                    String userToDelete = "Caio";
                    Console.Write("Excluindo usuário '" + userToDelete + "', pressione qualquer tecla para continuar...");
                    Console.WriteLine();
                    Console.ReadKey(true);

                    sb.Clear();
                    sb.Append("DELETE FROM Funcionarios WHERE Nome = @Nome;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " linha(s) deletada");
                    }

                    //_______________________________________
                    //_______________________________________
                    //_______________________________________
                    // Listar demo
                    Console.WriteLine("Ler dados da tabela, pressione qualquer tecla para continuar...");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    sql = "SELECT Id, Nome, Localizacao FROM Funcionarios;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Tudo feito...");
        }
    }
}
