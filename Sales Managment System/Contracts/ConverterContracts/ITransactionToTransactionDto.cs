using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts.ConverterContracts;

public interface ITransactionToTransactionDto
{
    public TransactionDto ToTransactionDto(Transaction tr);
}