namespace datasheetapi.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(object value) => Value = value;

    public object Value { get; }
}
