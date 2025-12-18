using Sales_Managment_System.Models;

namespace Sales_Managment_System.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }
    public TimeOnly Time { get; set; }
    public Guid ServiceGuid { get; set; }
    public String CarNumber { get; set; }

    public static TransactionDto ToTransactionDto(Transaction tr)
    {
        return new TransactionDto()
        {
            Id = tr.Id,
            Time = tr.Time,
            ServiceGuid = tr.ServiceId,
            CarNumber = tr.CarNumber
        };
    }
}