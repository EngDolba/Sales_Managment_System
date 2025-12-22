using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales_Managment_System.DTOs;

public class User
{
    [Key] public Guid   guid           { get; set; }
    public String username       { get; set; }
    public String hashedPassword { get; set; }
    public String roles          { get; set; }
}