using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CoffeeShopAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ILogger<TransactionController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<Dictionary<string, object>> Get()
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        List<Dictionary<string, object>> transactions = TransactionDB.ViewTransactions();

        return transactions;
    }

    [HttpGet]
    [Route("{id}")]
    public Dictionary<string, object> Get(int id)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        Dictionary<string, object> transaction = TransactionDB.ViewSpecificTransaction(id);

        return transaction;
    }

    [HttpPost]
    public void Post([FromBody] Dictionary<string, int> items, [FromQuery] String payment, [FromQuery] float totalCost, [FromQuery] String customerName)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        TransactionDB.NewTransaction(items, payment, totalCost, customerName);

        return;
    }

    [HttpPut]
    [Route("{id}")]
    public void Put(int id, [FromQuery] String payment = "", [FromQuery] String customerName = "")
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        TransactionDB.EditTransaction(id, payment, customerName);

        return;
    }

    [HttpPut]
    [Route("{id}/complete")]
    public void Put(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        TransactionDB.MarkTransactionComplete(id);

        return;
    }

    [HttpDelete]
    [Route("{id}")]
    public void Delete(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        TransactionDB.RemoveTransaction(id);

        return;
    }
}