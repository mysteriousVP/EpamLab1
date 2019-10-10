using System;
using System.Runtime.Serialization;

namespace PolynomialLibrary
{
    [Serializable]
    public class PolynomialException : Exception
    {
        public PolynomialException()
        {

        }

        public PolynomialException(string message) : base(message)
        {

        }

        public PolynomialException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public PolynomialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
