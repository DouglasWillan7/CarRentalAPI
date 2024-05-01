using System.Net;

namespace DW.Rental.Domain.Exception;

public class InvalidCategoryException : BaseException
{
    public InvalidCategoryException(string message, string? parameter = null) : base(message, ExceptionType.NotFound, HttpStatusCode.NotAcceptable, parameter) { }
}
