using Microsoft.IdentityModel.Tokens;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class DailyReportService(
    IDailyReportRepository repository,
    ITransactionService transactionService,
    IServiceService service) : IDailyReportService
{
    public void AddDailyReport(DailyReport dailyReport)
    {
        repository.AddDailyReport(dailyReport);
    }

    public DailyReport CloseDay(DateOnly date)
    {
        double income = 0;
        int carAmount = 0;
        IEnumerable<TransactionDto> list = transactionService.GetAllTransactions();
        if (!list.IsNullOrEmpty())
        {
            List<TransactionDto> transactions = list.ToList();
            List<Service> services = transactions
                .Select(t => service.GetService(t.ServiceGuid))
                .ToList();
            income = services.Sum(t => t.ServicePrice);
            carAmount = transactions.Count();
            transactionService.DeleteAllTransactions();
        }

        DailyReport dr = new()
        {
            CarNumbers = carAmount,
            Date = date,
            Sum = income
        };
        AddDailyReport(dr);
        return dr;
    }
}