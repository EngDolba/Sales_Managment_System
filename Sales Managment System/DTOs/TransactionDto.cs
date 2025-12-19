using Sales_Managment_System.Models;

namespace Sales_Managment_System.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }
    public TimeOnly Time { get; set; }
    public Guid ServiceGuid { get; set; }
    public string CarNumber { get; set; }

    public static TransactionDto ToTransactionDto(Transaction tr)
    {
        return new TransactionDto
        {
            Id = tr.Guid,
            Time = tr.Time,
            ServiceGuid = tr.ServiceGuid,
            CarNumber = tr.CarNumber
        };
    }
}