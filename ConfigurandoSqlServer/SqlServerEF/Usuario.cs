using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurandoSqlServer.SqlServerEF
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public String PrimeiroNome { get; set; }
        public String UltimoNome { get; set; }
        public virtual IList<Tarefa> Tarefas { get; set; }

        public String GetNomeInteiro()
        {
            return this.PrimeiroNome + " " + this.UltimoNome;
        }
        public override string ToString()
        {
            return "Usuario [id=" + this.UsuarioId + ", name=" + this.GetNomeInteiro() + "]";
        }
    }
}
