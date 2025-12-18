using Sales_Managment_System.DTOs;

namespace Sales_Managment_System.Contracts.ServiceContracts;

public interface ITransactionService
{
    public IEnumerable<TransactionDto> GetAllTransactions();
    public TransactionDto GetTransaction(Guid guid);
    public TransactionDto EditTransaction(Guid guid, TransactionDto transactionDto);
    public TransactionDto CreateTransaction(TransactionCreateDto transactionCreateDto);

    public void DeleteAllTransactions();
}

