using Microsoft.EntityFrameworkCore;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public Transaction? GetById(Guid id)
    { 
        Transaction? tr = context.tr_daily.Include(t => t.Service).FirstOrDefault(t => t.Id == id);
       return tr;
    }

    public IEnumerable<Transaction> GetAll()
    {
        var bla = context.tr_daily.Include(t => t.Service).ToList();
        return bla;
    }

    public void create(Transaction tr)
    {
        context.tr_daily.Add(tr);
        context.SaveChanges();
    }

    public void truncate()
    {
        context.Database.ExecuteSqlRaw("truncate table \"tr_daily\"");
    }
}