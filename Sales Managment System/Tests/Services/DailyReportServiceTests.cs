using AutoFixture;
using FluentAssertions;
using Moq;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;
using Sales_Managment_System.Services;
using Xunit;

namespace Sales_Managment_System.Tests.Services;

public class DailyReportServiceTests
{
    [Fact]
    public void CloseDay_SuccessfulCase()
    {
        //A1
        IFixture fixture = new Fixture();
        List<TransactionDto> transactions = fixture.Build<TransactionDto>()
            .CreateMany(2).ToList();
        Service? service1 = fixture.Build<Service>().With(t => t.ServicePrice, 100).Create();
        Service? service2 = fixture.Build<Service>().With(t => t.ServicePrice, 200).Create();

        DateOnly date = new(2025, 12, 18);
        Mock<ITransactionService> mTransactionService = new();
        mTransactionService.Setup(t => t.GetAllTransactions()).Returns(transactions);
        Mock<IServiceService> mService = new();
        Mock<IDailyReportRepository> mRepository = new();
        mRepository
            .Setup(r => r.AddDailyReport(It.IsAny<DailyReport>()));
        mService.Setup(s => s.GetService(transactions[0].ServiceGuid))
            .Returns(service1);
        mService.Setup(s => s.GetService(transactions[1].ServiceGuid))
            .Returns(service2);
        //A2
        DailyReportService reportService = new(mRepository.Object, mTransactionService.Object, mService.Object);
        //A3
        DailyReport dailyReport = reportService.CloseDay(date);
        dailyReport.Sum.Should().Be(300);
        dailyReport.CarNumbers.Should().Be(2);
        dailyReport.Should().NotBeNull();
    }
}