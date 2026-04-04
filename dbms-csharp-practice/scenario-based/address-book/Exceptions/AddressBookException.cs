namespace AddressBookSystem.Exceptions
{
    public class DuplicateContactException : Exception
    {
        public DuplicateContactException(string message) : base(message) { }
    }

    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(string message) : base(message) { }
    }

    public class AddressBookNotFoundException : Exception
    {
        public AddressBookNotFoundException(string message) : base(message) { }
    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }

    public class DataSourceException : Exception
    {
        public DataSourceException(string message) : base(message) { }
        public DataSourceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
