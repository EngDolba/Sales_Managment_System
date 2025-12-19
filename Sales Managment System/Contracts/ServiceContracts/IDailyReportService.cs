using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts;

public interface IDailyReportService
{
    public void AddDailyReport(DailyReport dailyReport);
    public DailyReport CloseDay(DateOnly date);
}