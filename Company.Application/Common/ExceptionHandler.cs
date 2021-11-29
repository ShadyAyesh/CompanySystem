using System;

namespace CompanySystem.Application.Common
{
    public static class ExceptionHandler
    {
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string name, object key)
            : base($"{name} with Id: ({key}) is not found.")
        {
        }
    }

    public class InvalidModelException : Exception
    {
        public InvalidModelException()
            : base("Invalid Model.")
        {
        }
    }

    public class DefaultException : Exception
    {
        public DefaultException(string message)
            : base(message)
        {
        }
    }
}