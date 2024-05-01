using System.Net;

namespace DW.Rental.Domain.Exception;

public class BaseException : System.Exception
{
    protected BaseException(
    string message,
    string code,
    HttpStatusCode statusCode,
    string? parameter,
    System.Exception? innerException = null
  ) : base(message, innerException)
    {
        Code = code;
        StatusCode = statusCode;
        Parameter = parameter;
    }

    public string Code { get; }
    public string? Parameter { get; }
    public HttpStatusCode StatusCode { get; }
}
