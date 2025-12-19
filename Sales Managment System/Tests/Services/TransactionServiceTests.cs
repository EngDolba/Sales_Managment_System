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
        Fixture fix = new Fixture();
        var transaction = fix.Create<TransactionCreateDto>();
        
        Mock<ITransactionRepository> mITransactionRepository = new Mock<ITransactionRepository>();
        Mock<IServiceService> mIService = new Mock<IServiceService>();
        Mock<ITransactionToTransactionDto> mtoTransactionDto = new Mock<ITransactionToTransactionDto>();

        mIService.Setup(s => s.GetService(It.IsAny<Guid>()))
            .Returns(null as Service);
        TransactionService ts = new TransactionService(mITransactionRepository.Object, mIService.Object,mtoTransactionDto.Object);
        Action act = () => ts.CreateTransaction(transaction);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void createTransactino_successfulCase()
    {
        Fixture fix = new Fixture();
        var transactionCreateDto = fix.Create<TransactionCreateDto>();
        var service = fix.Create<Service>();
        var transaction= new Transaction()
        {
            CarNumber = transactionCreateDto.CarNumber,
            ServiceGuid = transactionCreateDto.ServiceGuid,
            Time = transactionCreateDto.Time,
            Service = service,
            Guid = Guid.NewGuid()
        };
        var transactionDto = new TransactionDto()
        {
            CarNumber = transaction.CarNumber,
            ServiceGuid = transaction.ServiceGuid,
            Time = transaction.Time,
            Id = transaction.Guid
        };
        
        Mock<IServiceService> mIService = new Mock<IServiceService>();
        mIService.Setup(s => s.GetService(It.IsAny<Guid>()))
            .Returns(service);
        Mock<ITransactionRepository> mITransactionRepository = new Mock<ITransactionRepository>();
        Mock<ITransactionToTransactionDto> mtoTransactionDto = new Mock<ITransactionToTransactionDto>();
        mtoTransactionDto
            .Setup(t => t.ToTransactionDto(It.Is<Transaction>(tr =>
                    tr.ServiceGuid == transaction.ServiceGuid &&
                    tr.Service == transaction.Service &&
                    tr.CarNumber == transaction.CarNumber &&
                    tr.Time == transaction.Time
            )))
            .Returns(transactionDto);        
        TransactionService trs = new TransactionService(mITransactionRepository.Object, mIService.Object, mtoTransactionDto.Object);
        var tr = trs.CreateTransaction(transactionCreateDto);
        tr.Should().NotBeNull();
        tr.ServiceGuid.Should().NotBe(Guid.Empty);
        tr.CarNumber.Should().Be(transaction.CarNumber);
        tr.Time.Should().Be(transaction.Time);
        
        
        




    }
    
}