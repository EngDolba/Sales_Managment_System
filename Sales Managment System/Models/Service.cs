using System.ComponentModel.DataAnnotations;

namespace Sales_Managment_System.Models;

public class Service
{
    [Key] public Guid Id { get; set; }

    [Required] public string ServiceName { get; set; } = string.Empty;

    [Required] public string ServiceType { get; set; } = string.Empty;

    [Required] public double ServicePrice { get; set; }
}