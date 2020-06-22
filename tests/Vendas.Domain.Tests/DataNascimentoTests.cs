using System;
using Vendas.Domain.ClienteAggregate;
using Vendas.Domain.ClienteAggregate.Exceptions;
using Xunit;

namespace Vendas.Domain.Tests
{
    public class DataNascimentoTests
    {
        [Fact]
        public void CriarDataNascimento()
        {
            DataNascimento.DataAtual = new DateTime(2020, 6, 21);

            var data = new DateTime(1986, 2, 26);

            var dataNascimento = DataNascimento.Criar(data);

            var dataNascimentoEsperada = new DataNascimento(data);
            Assert.Equal(dataNascimentoEsperada, dataNascimento);
        }

        [Fact]
        public void CriarDataNascimentoMenor()
        {
            DataNascimento.DataAtual = new DateTime(2020, 6, 21);

            var data = new DateTime(2010, 2, 26);

            var exception = Assert.Throws<DataNascimentoInvalidaException>(() => DataNascimento.Criar(data));
        }
    }
}