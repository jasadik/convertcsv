[Serializable]
public class BadUriException : Exception
{
    public BadUriException() { }

    public BadUriException(string message)
        : base(message) { }

    public BadUriException(string message, Exception inner)
        : base(message, inner) { }
}