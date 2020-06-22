namespace Vendas.Domain.ClienteAggregate
{
    public class Cliente
    {
        public CPF CPF { get; private set; }
        public DataNascimento DataNascimento { get; private set; }

        private Cliente() { }
        
        private Cliente(CPF cpf, DataNascimento dataNascimento)
        {
            CPF = cpf;
            DataNascimento = dataNascimento;
        }

        public static Cliente Criar(CPF cpf, DataNascimento dataNascimento)
        {
            if (cpf is null)
                throw new System.ArgumentNullException(nameof(cpf));

            if (dataNascimento is null)
                throw new System.ArgumentNullException(nameof(dataNascimento));

            return new Cliente(cpf, dataNascimento);
        }
    }
}