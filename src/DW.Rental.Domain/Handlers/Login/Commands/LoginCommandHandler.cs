using DW.Rental.Domain.Configs;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Login;
using DW.Rental.Shareable.Responses.Login;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OperationResult;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DW.Rental.Domain.Handlers.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeliverymanRepository _deliverymanRepository;
    private readonly string _secret;

    public LoginCommandHandler(IUserRepository motorcycleRepository, IDeliverymanRepository deliverymanRepository, IOptions<JwtSettings> jwtSettings)
    {
        _deliverymanRepository = deliverymanRepository;
        _userRepository = motorcycleRepository;
        _secret = jwtSettings.Value.SecretKey!;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(loginRequest.Username, loginRequest.Password, cancellationToken);

        if (user is null)
            return Result.Error<LoginResponse>(new NotFoundException("User not found"));

        if (user.Role == Shareable.Enum.RoleTypeEnum.Deliveryman)
        {
            user.DeliverymanDomain = await _deliverymanRepository.GetByUserIdAsync(user.Id, cancellationToken);
        }

        var token = GenerateToken(user);

        return Result.Success(new LoginResponse(token));
    }

    private string GenerateToken(UserDomain user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim("deliverymanId", user.DeliverymanDomain == null ? "" : user.DeliverymanDomain.Id.ToString()),
                    new Claim("cnh_type", user.DeliverymanDomain == null ? "": user.DeliverymanDomain.CnhType.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
