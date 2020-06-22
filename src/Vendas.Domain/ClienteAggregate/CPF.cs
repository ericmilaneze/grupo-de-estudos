using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Vendas.Domain.ClienteAggregate.Exceptions;

[assembly: InternalsVisibleTo("Vendas.Domain.Tests")]

namespace Vendas.Domain.ClienteAggregate
{
    public class CPF
    {
        private const string PATTERN_ONZE_NUMEROS = @"[\d]{11}";
        private const string PATTERN_SOMENTE_NUMEROS = @"[^\d]";
        private const string PATTERN_NUMEROS_IGUAIS = @"([\d]){1}\1{10}";

        private string numeroCpf;

        private CPF() { }

        internal CPF(string numeroCpf)
        {
            this.numeroCpf = numeroCpf;
        }

        public static CPF Criar(string numeroCpf)
        {
            if (string.IsNullOrWhiteSpace(numeroCpf))
                throw new ArgumentException($"'{nameof(numeroCpf)}' cannot be null or whitespace", nameof(numeroCpf));

            string numeroCpfSemFormatacao = TirarFormatacaoCpf(numeroCpf);
            var cpf = new CPF(numeroCpfSemFormatacao);
            cpf.Validar();
            return cpf;
        }

        public void Validar()
        {
            ValidarSeCpfTemOnzeNumeros();
            ValidarSeNumerosSaoIguais();
            ValidarNumeroCpf();
        }

        public string ObterCpfSemFormatacao() =>
            TirarFormatacaoCpf(numeroCpf.Trim());

        public static string TirarFormatacaoCpf(string cpfFormatado) =>
            Regex.Replace(cpfFormatado.Trim(), PATTERN_SOMENTE_NUMEROS, "");

        private void ValidarNumeroCpf()
        {
            bool isCpfValido = IsCpfValido();
            if (isCpfValido == false)
                throw new CPFInvalidoException($"CPF inválido: {numeroCpf}");
        }

        private bool IsCpfValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpf = ObterCpfSemFormatacao();

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private void ValidarSeCpfTemOnzeNumeros()
        {
            bool cpfTemOnzeNumeros = TemOnzeNumeros();
            if (cpfTemOnzeNumeros == false)
                throw new CPFInvalidoException($"CPF não contém 11 números: {numeroCpf}");
        }

        private void ValidarSeNumerosSaoIguais()
        {
            bool cpfComNumerosIguais = NumerosSaoIguais();
            if (cpfComNumerosIguais == false)
                throw new CPFInvalidoException($"O CPF está com os 11 números iguais: {numeroCpf}");
        }

        private bool NumerosSaoIguais() =>
            !IsMatch(PATTERN_NUMEROS_IGUAIS);

        private bool TemOnzeNumeros() =>
            IsMatch(PATTERN_ONZE_NUMEROS);

        private bool IsMatch(string pattern)
        {
            string cpfSemFormatacao = ObterCpfSemFormatacao();
            return Regex.IsMatch(cpfSemFormatacao, pattern);
        }

        public override bool Equals(object obj) =>
            obj is CPF cpf &&
            numeroCpf == cpf.numeroCpf;

        public override int GetHashCode() =>
            HashCode.Combine(numeroCpf);

        public static implicit operator string(CPF cpf) =>
            cpf?.ObterCpfSemFormatacao();

        public static implicit operator CPF(string numeroCpf) =>
            Criar(numeroCpf);
    }
}