using Sales_Managment_System.DTOs;

namespace Sales_Managment_System.Contracts;

public interface IAuthenticationRepository
{
    public User getUser(UserLoginDto user);
    public void addUser(User user);

    public bool checkForUsernameExistence(string username);
}