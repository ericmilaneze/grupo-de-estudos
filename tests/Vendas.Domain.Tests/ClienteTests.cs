using System;
using Vendas.Domain.ClienteAggregate;
using Xunit;

namespace Vendas.Domain.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void CriarCliente()
        {
            var cpf = CPF.Criar("066.099.240-07");
            var dataNascimento = DataNascimento.Criar(new DateTime(1986, 2, 26));
            var cliente = Cliente.Criar(cpf, dataNascimento);

            Assert.IsType<Cliente>(cliente);
        }
    }
}