using System;
using System.Runtime.Serialization;

namespace Lib.Exceptions
{
    [Serializable]
    public class MyException : Exception
    {
        public MyException()
        {
        }

        public MyException(string message)
        : base(String.Format("MyException: {0}", message))
        {
        }

        public MyException(string message, Exception inner)
        : base(String.Format("MyException: {0}", message), inner)
        {
        }

        public MyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}