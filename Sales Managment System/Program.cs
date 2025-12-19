using Microsoft.EntityFrameworkCore;
using Sales_Managment_System;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ConverterContracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.Repositories;
using Sales_Managment_System.Services;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("Logs").CreateLogger();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

builder.Services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
builder.Services.AddScoped(typeof(ITransactionService), typeof(TransactionService));
builder.Services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
builder.Services.AddScoped(typeof(IDailyReportRepository), typeof(DailyReportRepository));
builder.Services.AddScoped(typeof(IDailyReportService), typeof(DailyReportService));
builder.Services.AddScoped(typeof(IServiceService), typeof(ServiceService));
builder.Services.AddScoped(typeof(ITransactionToTransactionDto), typeof(ITransactionToTransactionDto));

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();