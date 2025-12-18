using Sales_Managment_System.Contracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class ServiceService(IServiceRepository repository, 
    ITransactionRepository transactionRepository, DailyReportService dailyReportService) : IServiceService
{
    public Service GetService(Guid guid)
    {
        return repository.getById(guid);
    }
    public DailyReport CloseDay(DateOnly date)
    {
        IEnumerable<Transaction> list = transactionRepository.GetAll();
        IEnumerable<Transaction> transactions = list.ToList();
        int income = (int)transactions.Sum(t => t.Service.ServicePrice);
        int carAmount = transactions.Count();
        transactionRepository.truncate();
        DailyReport dr = new DailyReport()
        {
            CarNumbers = carAmount,
            Date = date,
            Sum = income
        };
        dailyReportService.AddDailyReport(dr);
        return dr;

    }
}
