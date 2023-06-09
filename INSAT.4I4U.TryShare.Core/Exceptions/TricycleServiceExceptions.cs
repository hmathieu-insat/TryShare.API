﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INSAT._4I4U.TryShare.Core.Exceptions
{

    [Serializable]
    public class TricycleNotAvailableException : Exception
    {
        public TricycleNotAvailableException() { }
        public TricycleNotAvailableException(string message) : base(message) { }
        public TricycleNotAvailableException(string message, Exception inner) : base(message, inner) { }
        protected TricycleNotAvailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class TricycleAvailableException : Exception
    {
        public TricycleAvailableException() { }
        public TricycleAvailableException(string message) : base(message) { }
        public TricycleAvailableException(string message, Exception inner) : base(message, inner) { }
        protected TricycleAvailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class TricycleNotFoundException : Exception
    {
        public TricycleNotFoundException() { }
        public TricycleNotFoundException(string message) : base(message) { }
        public TricycleNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected TricycleNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
