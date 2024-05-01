using DW.Rental.Shareable.Responses.Login;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Login;

public record LoginRequest(string Username, string Password) : IRequest<Result<LoginResponse>>;
