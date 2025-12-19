using Sales_Managment_System.Contracts.ConverterContracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.DTOs;

public class TransactionToTransactionDto : ITransactionToTransactionDto
{
    public TransactionDto ToTransactionDto(Transaction tr)
    {
        return TransactionDto.ToTransactionDto(tr);
    }
}