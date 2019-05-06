using System;

namespace CsvImporter.Common.Contracts.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message)
        {
        }

        public abstract int StatusCode { get; }
    }
}