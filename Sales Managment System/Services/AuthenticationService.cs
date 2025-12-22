using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Exceptions;

namespace Sales_Managment_System.Services;

public class AuthenticationService(IAuthenticationRepository repo,AppDbContext context) : IAuthenticationService
{
    public String? login(UserLoginDto userLoginDto,IConfiguration configuration)
    {
        var passwordHasher = new PasswordHasher<UserLoginDto>();
        var user = repo.getUser(userLoginDto);
        if (user is null)
        {
            throw new InvalidCredentialException();

        }
        var passwordVerificationResult =
            passwordHasher.VerifyHashedPassword(userLoginDto, user.hashedPassword, userLoginDto.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Success)
        {
            var token = CreateToken(userLoginDto, configuration);
            return token;
        }
        else
        {
            throw new InvalidCredentialException();
        }
    }

    public User register(UserRegistrationDto userRegistrationDto)
    {
        using var transaction = context.Database.BeginTransaction();
        if (repo.checkForUsernameExistence(userRegistrationDto.Username))
        {
            throw new UserAlreadyRegisteredException();
        }
        
        var user = new User()
        {

            guid = Guid.NewGuid(),
            username = userRegistrationDto.Username,
            hashedPassword =
                (new PasswordHasher<UserRegistrationDto>()).HashPassword(userRegistrationDto,
                    userRegistrationDto.Password),
            roles = ""
        };
       
        repo.addUser(user);
        transaction.Commit();
        return user;
        
    }
    private string CreateToken(UserLoginDto user,IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}