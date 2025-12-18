using Sales_Managment_System.Contracts;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Repositories;

public class DailyReportRepository(AppDbContext context) : IDailyReportRepository
{
    public void AddDailyReport(DailyReport dr)
    {
            
            context.daily_reports.Add(dr);
            context.SaveChanges();
    }
}