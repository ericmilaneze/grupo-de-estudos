using System;
using Vendas.Domain.ClienteAggregate.Exceptions;

namespace Vendas.Domain.ClienteAggregate
{
    public class DataNascimento
    {
        public static DateTime? DataAtual;

        private DateTime dataNascimento;

        internal DataNascimento(DateTime dataNascimento)
        {
            this.dataNascimento = dataNascimento;
        }

        public static DataNascimento Criar(DateTime dataNascimento)
        {
            var obj = new DataNascimento(dataNascimento);
            obj.Validar();
            return obj;
        }

        public void Validar()
        {
            int idade = ObterIdade();
            if (idade < 18)
                throw new DataNascimentoInvalidaException($"Cliente deve ter ser maior de 18. Idade passada: {idade}");
        }

        public int ObterIdade()
        {
            DateTime dataAtual = ObterDataAtual();
            return ObterIdade(dataAtual);
        }

        public DateTime ObterDataNascimento() =>
            dataNascimento;

        private int ObterIdade(DateTime dataAtual)
        {
            var idade = dataAtual.Year - dataNascimento.Year;
            if (dataNascimento.Date > dataAtual.AddYears(-idade))
                return idade - 1;
            return idade;
        }

        private static DateTime ObterDataAtual() =>
            DataAtual ?? DateTime.UtcNow;

        public override bool Equals(object obj) =>
            obj is DataNascimento nascimento &&
            dataNascimento == nascimento.dataNascimento;

        public override int GetHashCode() =>
            HashCode.Combine(dataNascimento);

        public static implicit operator DataNascimento(DateTime dataNascimento) =>
            Criar(dataNascimento);

        public static implicit operator DateTime(DataNascimento dataNascimento) =>
            dataNascimento?.ObterDataNascimento() ?? DateTime.MinValue;
    }
}