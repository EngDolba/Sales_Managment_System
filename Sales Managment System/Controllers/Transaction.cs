using Microsoft.AspNetCore.Authorization;
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
    IDailyReportService dailyReportService) : ControllerBase
{
    [Route("/")]
    [Route("getAll")]
    [Authorize]
    [HttpGet]
    [Authorize(Roles = "Manager")]
    public ActionResult<IEnumerable<TransactionDto>> GetAllTransactions()
    {
        logger.LogInformation("Received request: GetAllTransactions endpoint called.");
        IEnumerable<TransactionDto> transactionDtos = transactionService.GetAllTransactions();
        return Ok(transactionDtos);
    }

    [Route("get/{guid}")]
    
    [HttpGet]
    [Authorize(Roles = "Salesperson,Manager")]
    public ActionResult<TransactionDto> GetTransaction(Guid guid)
    {
        TransactionDto transactionDto;
        logger.LogInformation($"Received request: GetTransactions endpoint called. Arguments: Guid:{guid}", guid);
        try
        {
             transactionDto = transactionService.GetTransaction(guid);
        }
        catch (ArgumentException e)
        {
            return BadRequest("No Such Transaction Was Found In Database");
        }

        return Ok(transactionDto);
    }

    [Route("add")]
    [Authorize(Roles = "Salesperson,Manager")]
    [HttpPost]
    public ActionResult<TransactionDto> AddTransaction(TransactionCreateDto transactionCreateDto)
    {
        TransactionDto td = transactionService.CreateTransaction(transactionCreateDto);
        return Ok(td);
    }

    [HttpPost]
    [Route("/generateDailyReport")]
    [Authorize(Roles = "Manager")]
    public ActionResult<DailyReport> CloseDay(DateOnly date)
    {
        DailyReport dr = dailyReportService.CloseDay(date);
        return Ok(dr);
    }
}