using ConfigurandoSqlServer.SqlServerEF.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ConfigurandoSqlServer.SqlServerEF.Servicos
{
    public class EFConexao : IOpcoes
    {
        public void Executar()
        {
            Console.WriteLine("** C# CRUD Entity Framework e SQL Server **\n");
            try
            {
                // Construir cadeia de conexão
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-UA5T9LL\\SQLEXPRESS";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "104059";      // update me
                builder.InitialCatalog = "EFExemploSimplesDB";

                using (EFContext context = new EFContext(builder.ConnectionString))
                {
                    Console.WriteLine("Esquema de banco de dados criado a partir do C#.");

                    // Criar demonstração: crie uma instância de usuário e salve-a no banco de dados
                    Usuario NovoUsuario = new Usuario { PrimeiroNome = "Anna", UltimoNome = "Paula" };
                    context.Usuarios.Add(NovoUsuario);
                    context.SaveChanges();
                    Console.WriteLine("\nUsuário criado: " + NovoUsuario.ToString());


                    //Criar demonstração: crie uma instância de tarefa e salve-a no banco de dados
                    Tarefa NovaTarefa = new Tarefa() { Titulo = "Limpar Navio", IsComplete = false, DataDeVencimento = new DateTime(2017, 04, 01) };
                    context.Tarefas.Add(NovaTarefa);
                    context.SaveChanges();
                    Console.WriteLine("\nTarefa criada: " + NovaTarefa.ToString());

                    // Demonstração de associação: atribuir tarefa ao usuário
                    NovaTarefa.Usuario = NovoUsuario;
                    context.SaveChanges();

                    Console.WriteLine("\nUsuario Tarefa: '" + NovaTarefa.ToString() + "' para usuario '" + NovoUsuario.GetNomeInteiro() + "'");

                    // Leia a demonstração: encontre tarefas incompletas atribuídas ao usuário 'Anna'
                    Console.WriteLine("\nTarefas incompletas atribuídas a 'Anna':");
                    var query = from t in context.Tarefas
                                where t.IsComplete == false &&
                                t.Usuario.PrimeiroNome.Equals("Anna")
                                select t;

                    foreach (var t in query)
                    {
                        Console.WriteLine(t.ToString());
                    }

                    //Atualização: altere a 'data de vencimento' de uma tarefa
                    Tarefa tarefaAtualizar = context.Tarefas.First(); // get pegar primeira tarefa

                    Console.WriteLine("\natualizando tarefa: " + tarefaAtualizar.ToString());
                    tarefaAtualizar.DataDeVencimento = new DateTime(2016,06,30);
                    context.SaveChanges();
                    Console.WriteLine("data de vencimento mudada: " + tarefaAtualizar.ToString());

                    // Excluir: exclua todas as tarefas com data de vencimento em 2016
                    Console.WriteLine("\nExcluindo todas as tarefas com uma data de vencimento em 2016");
                    DateTime datavencimento2016 = new DateTime(2016, 06, 30);
                    query = from t in context.Tarefas
                            where t.DataDeVencimento < datavencimento2016
                            select t;

                    foreach (Tarefa t in query)
                    {
                        Console.WriteLine("deletando tarefa: " + t.ToString());
                        context.Tarefas.Remove(t);
                    }
                    context.SaveChanges();

                    // Mostrar tarefas após a operação 'Excluir' - deve haver 0 tarefas
                    Console.WriteLine("\nTarefas após a exclusão:");
                    List<Tarefa> tasksAfterDelete = (from t in context.Tarefas select t).ToList<Tarefa>();
                    if (tasksAfterDelete.Count == 0)
                    {
                        Console.WriteLine("[Nenhum]");
                    }
                    else
                    {
                        foreach (Tarefa t in query)
                        {
                            Console.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
