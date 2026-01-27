using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeAluguel.Domain.ValueObjects
{
    public record   Dinheiro
    {
        public decimal Valor { get; }
        public Dinheiro(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero", nameof(valor));
            Valor = valor;
        }

        public Dinheiro Somar(Dinheiro outro)
        {
            return new Dinheiro(Valor + outro.Valor);
        }

        public Dinheiro Subtrair(Dinheiro outro)
        {
            return new Dinheiro(Valor - outro.Valor);
        }

        public Dinheiro Multiplicar(decimal fator)
        {
            return new Dinheiro(Valor * fator);
        }

        public Dinheiro Dividir(decimal divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Não é possível dividir por zero.");
            return new Dinheiro(Valor / divisor);
        }
    }
}
