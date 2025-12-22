using AutoFixture;
using FluentAssertions;
using Moq;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ConverterContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;
using Sales_Managment_System.Services;
using Xunit;

namespace Sales_Managment_System.Tests.Services;

public class TransactionServiceTests
{
    [Fact]
    public void CreateTransaction_serviceIsNull_throwsException()
    {
        Fixture fix = new();
        TransactionCreateDto? transaction = fix.Create<TransactionCreateDto>();

        Mock<ITransactionRepository> mITransactionRepository = new();
        Mock<IServiceService> mIService = new();
        Mock<ITransactionToTransactionDto> mtoTransactionDto = new();

        mIService.Setup(s => s.GetService(It.IsAny<Guid>()))
            .Returns(null as Service);
        TransactionService ts = new(mITransactionRepository.Object, mIService.Object, mtoTransactionDto.Object);
        Action act = () => ts.CreateTransaction(transaction);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void createTransaction_successfulCase()
    {
        Fixture fix = new();
        TransactionCreateDto? transactionCreateDto = fix.Create<TransactionCreateDto>();
        Service? service = fix.Create<Service>();
        Transaction transaction = new()
        {
            CarNumber = transactionCreateDto.CarNumber,
            ServiceGuid = transactionCreateDto.ServiceGuid,
            Time = transactionCreateDto.Time,
            Service = service,
            Guid = Guid.NewGuid()
        };
        TransactionDto transactionDto = new()
        {
            CarNumber = transaction.CarNumber,
            ServiceGuid = transaction.ServiceGuid,
            Time = transaction.Time,
            Id = transaction.Guid
        };

        Mock<IServiceService> mIService = new();
        mIService.Setup(s => s.GetService(It.IsAny<Guid>()))
            .Returns(service);
        Mock<ITransactionRepository> mITransactionRepository = new();
        Mock<ITransactionToTransactionDto> mtoTransactionDto = new();
        mtoTransactionDto
            .Setup(t => t.ToTransactionDto(It.Is<Transaction>(tr =>
                tr.ServiceGuid == transaction.ServiceGuid &&
                tr.Service == transaction.Service &&
                tr.CarNumber == transaction.CarNumber &&
                tr.Time == transaction.Time
            )))
            .Returns(transactionDto);
        TransactionService trs = new(mITransactionRepository.Object, mIService.Object, mtoTransactionDto.Object);
        TransactionDto tr = trs.CreateTransaction(transactionCreateDto);
        tr.Should().NotBeNull();
        tr.ServiceGuid.Should().NotBe(Guid.Empty);
        tr.CarNumber.Should().Be(transaction.CarNumber);
        tr.Time.Should().Be(transaction.Time);
    }
}