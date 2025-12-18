using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts;

public interface ITransactionRepository
{
    Transaction? GetById(Guid id);
    IEnumerable<Transaction> GetAll();
    void create(Transaction tr);
    void truncate();
}