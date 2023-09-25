namespace datasheetapi.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(object value) => Value = value;

    public object Value { get; }
}
