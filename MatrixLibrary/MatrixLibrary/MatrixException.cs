using System;
using System.Runtime.Serialization;

namespace MatrixLibrary
{
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException()
        {

        }

        public MatrixException(string message) : base(message)
        {

        }

        public MatrixException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public MatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
