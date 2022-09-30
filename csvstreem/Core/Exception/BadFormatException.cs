[Serializable]
public class BadFormatException : Exception
{
    public BadFormatException() { }

    public BadFormatException(string message)
        : base(message) { }

    public BadFormatException(string message, Exception inner)
        : base(message, inner) { }
}