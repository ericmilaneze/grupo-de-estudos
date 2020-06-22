using System;
using System.Runtime.Serialization;

namespace Vendas.Domain.ClienteAggregate.Exceptions
{
    [Serializable]
    internal class DataNascimentoInvalidaException : Exception
    {
        public DataNascimentoInvalidaException()
        {
        }

        public DataNascimentoInvalidaException(string message) : base(message)
        {
        }

        public DataNascimentoInvalidaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataNascimentoInvalidaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}