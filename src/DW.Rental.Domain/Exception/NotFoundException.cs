using System.Net;

namespace DW.Rental.Domain.Exception;

public class NotFoundException : BaseException
{
    public NotFoundException(string message, string? parameter = null) : base(message, ExceptionType.NotFound, HttpStatusCode.NotFound, parameter) { }
}
