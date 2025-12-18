using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class DailyReportService(IDailyReportRepository repository,ITransactionService transactionService,IServiceService service) : IDailyReportService
{

    public void AddDailyReport(DailyReport dailyReport)
    {
        repository.AddDailyReport(dailyReport);
    }
    public DailyReport CloseDay(DateOnly date)
    {
        IEnumerable<TransactionDto> list = transactionService.GetAllTransactions();
        IEnumerable<TransactionDto> transactions = list.ToList();
        // Assuming GetService returns a ServiceDto (or similar)
        List<Service> services = transactions
            .Select(t => service.GetService(t.ServiceGuid))
            .ToList();
        double income = services.Sum(t => t.ServicePrice);
        int carAmount = transactions.Count();
        transactionService.DeleteAllTransactions();
        DailyReport dr = new DailyReport()
        {
            CarNumbers = carAmount,
            Date = date,
            Sum = income
        };
        AddDailyReport(dr);
        return dr;

    }
}
