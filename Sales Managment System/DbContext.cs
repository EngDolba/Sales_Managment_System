using Microsoft.EntityFrameworkCore;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> tr_daily { get; set; }
    public DbSet<Service> services { get; set; }
    public DbSet<HistoricalTransactions> tr_hist { get; set; }
    public DbSet<DailyReport> daily_reports { get; set; }
    public DbSet<User> users { get; set; }
}