using System.Data.Entity;

namespace ConfigurandoSqlServer.SqlServerEF.Context
{
    public class EFContext : DbContext
    {
        public EFContext(string connectionString)
        {
            Database.SetInitializer<EFContext>(new DropCreateDatabaseAlways<EFContext>());
            this.Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
