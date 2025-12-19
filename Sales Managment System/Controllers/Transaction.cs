using Microsoft.AspNetCore.Mvc;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Models;

namespace Sales_Managment_System.Controllers;

[Route("transactions")]
[ApiController]
public class TransactionController(
    ITransactionService transactionService,
    ILogger<TransactionController> logger,
    IDailyReportService dailyReportService) : Controller
{
    [Route("/")]
    [Route("getAll")]
    [HttpGet]
    public ActionResult<IEnumerable<TransactionDto>> GetAllTransactions()
    {
        logger.LogInformation("Received request: GetAllTransactions endpoint called.");
        IEnumerable<TransactionDto> transactionDtos = transactionService.GetAllTransactions();
        return Ok(transactionDtos);
    }

    [Route("get/{guid}")]
    [HttpGet]
    public ActionResult<TransactionDto> GetTransaction(Guid guid)
    {
        logger.LogInformation($"Received request: GetTransactions endpoint called. Arguments: Guid:{guid}", guid);
        TransactionDto transactionDto = transactionService.GetTransaction(guid);
        return Ok(transactionDto);
    }

    [Route("add")]
    [HttpPost]
    public ActionResult<TransactionDto> AddTransaction(TransactionCreateDto transactionCreateDto)
    {
        TransactionDto td = transactionService.CreateTransaction(transactionCreateDto);
        return Ok(td);
    }

    [HttpPost]
    [Route("/generateDailyReport")]
    public ActionResult<DailyReport> CloseDay(DateOnly date)
    {
        DailyReport dr = dailyReportService.CloseDay(date);
        return Ok(dr);
    }
}