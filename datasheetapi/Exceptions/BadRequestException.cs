namespace datasheetapi.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(object value) => Value = value;

    public object Value { get; }
}
