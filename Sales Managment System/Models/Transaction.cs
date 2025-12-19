using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_Managment_System.Models;

public class Transaction
{
    [Key] public Guid Guid { get; set; }


    public Guid ServiceGuid { get; set; }
    public TimeOnly Time { get; set; }

    [ForeignKey(nameof(ServiceGuid))] public required Service Service { get; set; }

    [Length(6, 6)] public string? CarNumber { get; set; }
}