using Sales_Managment_System.Models;

namespace Sales_Managment_System.DTOs;

public class TransactionCreateDto
{
    public TimeOnly Time { get; set; }
    public Guid ServiceGuid { get; set; }
    public string CarNumber { get; set; }

    public static TransactionCreateDto ToTransactionDto(Transaction tr)
    {
        return new TransactionCreateDto
        {
            Time = tr.Time,
            ServiceGuid = tr.ServiceGuid,
            CarNumber = tr.CarNumber
        };
    }
}