using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record Periodo
    {
        public DateTime Inicio;
        public DateTime Fim;

        public Periodo(DateTime inicio, DateTime fim)
        {
            if (inicio >= fim)
                throw new ArgumentException("A data de fim deve ser posterior a de inicio");
            Inicio = inicio;
            Fim = fim;
        }
    }
}
