using Sales_Managment_System.DTOs;

namespace Sales_Managment_System.Contracts.ServiceContracts;

public interface IAuthenticationService
{
    public String? login(UserLoginDto userLoginDto,IConfiguration configuration);
    public User register(UserRegistrationDto userRegistrationDto);
}