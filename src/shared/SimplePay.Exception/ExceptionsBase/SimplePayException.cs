namespace SimplePay.Exception.ExceptionsBase;

public abstract class SimplePayException : System.Exception
{
    public abstract List<string> GetErrorMessages();
    public abstract int StatusCode();
}
