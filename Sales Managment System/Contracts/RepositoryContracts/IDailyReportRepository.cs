using Sales_Managment_System.Models;

namespace Sales_Managment_System.Contracts;

public interface IDailyReportRepository
{
    public void AddDailyReport(DailyReport dr);
}