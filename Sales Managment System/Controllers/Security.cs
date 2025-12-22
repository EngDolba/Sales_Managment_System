using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Exceptions;

namespace Sales_Managment_System.Controllers;

[Route("security")]
[ApiController]
public class SecurityController(IAuthenticationService authService) : ControllerBase
{
    [Route("login")]
    [HttpPost]
    public ActionResult<String?> Login(UserLoginDto userLoginDto,IConfiguration configuration)
    {
        String? s;
        User? user;
        try
        {
            
            s = authService.login(userLoginDto, configuration);
        }
        catch(InvalidCredentialException e)
        {
            return BadRequest("Invalid Credentials");
        }

        return Ok(s);
    
    }
    [Route("register")]
    [HttpPost]
    public ActionResult<User> Register(UserRegistrationDto userRegistrationDto)
    {
        User user;
        try
        {
            user = authService.register(userRegistrationDto);
        }
        catch (UserAlreadyRegisteredException e)
        {
            return BadRequest("Such Username Already Exists");
        }

        return Ok(user);
    }
    
}