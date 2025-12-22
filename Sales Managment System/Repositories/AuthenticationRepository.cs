using Microsoft.AspNetCore.Identity;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.DTOs;

namespace Sales_Managment_System.Repositories;

public class AuthenticationRepository(AppDbContext context) : IAuthenticationRepository
{
    public User getUser(UserLoginDto user)
    {
        return context.users.FirstOrDefault(t => t.username == user.Username);
    }

    public bool checkForUsernameExistence(string username)
    {

        return context.users.Count(u => u.username == username) > 0;

    }

    public void addUser(User user)
    {
        context.users.Add(user);
    }
}