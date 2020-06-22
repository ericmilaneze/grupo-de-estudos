using Vendas.Domain.ClienteAggregate;
using Vendas.Domain.ClienteAggregate.Exceptions;
using Xunit;

namespace Vendas.Domain.Tests
{
    public class CPFTests
    {
        [Theory]
        [InlineData("122.813.000-05", "12281300005")]
        [InlineData("640.841.150-96", "64084115096")]
        [InlineData("617.208.070-94", "61720807094")]
        public void CriarCPF(string numeroCpf, string expected)
        {
            var cpf = CPF.Criar(numeroCpf);
            
            var cpfEsperado = new CPF(expected);
            Assert.Equal(cpfEsperado, cpf);
        }

        [Theory]
        [InlineData("122.813.000-0")]
        [InlineData("11111111111")]
        [InlineData("617.208.070-95")]
        public void CriarCPFInvalido(string numeroCpf)
        {
            var exception = Assert.Throws<CPFInvalidoException>(() => CPF.Criar(numeroCpf));
        }
    }
}