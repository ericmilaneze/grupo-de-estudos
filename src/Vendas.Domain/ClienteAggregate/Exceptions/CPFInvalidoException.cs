using System;
using System.Runtime.Serialization;

namespace Vendas.Domain.ClienteAggregate.Exceptions
{
    [Serializable]
    internal class CPFInvalidoException : Exception
    {
        public CPFInvalidoException()
        {
        }

        public CPFInvalidoException(string message) : base(message)
        {
        }

        public CPFInvalidoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CPFInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}