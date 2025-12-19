using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ConverterContracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Services;

public class TransactionService(
    ITransactionRepository transactionRepository,
    IServiceService serviceService,
    ITransactionToTransactionDto toTransactionDto) : ITransactionService
{
    public IEnumerable<TransactionDto> GetAllTransactions()
    {
        IEnumerable<Transaction> transactions = transactionRepository.GetAll();
        List<TransactionDto> n = transactions.Select(entity => new TransactionDto
        {
            Id = entity.Guid,
            Time = entity.Time,
            ServiceGuid = entity.ServiceGuid,
            CarNumber = entity.CarNumber
        }).ToList();
        return n;
    }

    public TransactionDto GetTransaction(Guid guid)
    {
        Transaction? tr = transactionRepository.GetById(guid);
        if (tr is null) throw new ArgumentException("ID Is not Correct");
        return TransactionDto.ToTransactionDto(tr);
    }

    public TransactionDto EditTransaction(Guid guid, TransactionDto transactionDto)
    {
        throw new NotImplementedException();
    }

    public TransactionDto CreateTransaction(TransactionCreateDto dto)
    {
        Service? service = serviceService.GetService(dto.ServiceGuid);
        if (service is null) throw new ArgumentException("Such Service Does not exist");

        Transaction tr = new()
        {
            Guid = Guid.NewGuid(),
            Service = service,
            ServiceGuid = dto.ServiceGuid,
            Time = dto.Time,
            CarNumber = dto.CarNumber
        };
        transactionRepository.create(tr);
        return toTransactionDto.ToTransactionDto(tr);
    }

    public void DeleteAllTransactions()
    {
        transactionRepository.truncate();
    }
}