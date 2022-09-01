using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurandoSqlServer.SqlServerEF
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string Titulo { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public bool IsComplete { get; set; }
        public virtual Usuario Usuario { get; set; }

        public override string ToString()
        {
            return "Tarefa [id=" + this.TarefaId + ", Titulo=" + this.Titulo + ", Data de vencimento =" + this.DataDeVencimento.ToString() + ", Está completa=" + this.IsComplete + "]";
        }
    }
}
