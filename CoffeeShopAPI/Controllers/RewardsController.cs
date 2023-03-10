using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CoffeeShopAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class RewardController : ControllerBase
{
    private readonly ILogger<RewardController> _logger;

    public RewardController(ILogger<RewardController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{id}")]
    public Dictionary<string, string> Get(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        Dictionary<string, string> customer = RewardsDB.FindCustomer(id);

        return customer;
    }

    [HttpPost]
    public void Post([FromQuery] String customerName, [FromQuery] String phoneNumber, [FromQuery] String email = "")
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        RewardsDB.AddNewCustomer(customerName, phoneNumber, email);

        return;
    }

    [HttpPut]
    [Route("{id}")]
    public void Put(int id, [FromQuery] String phoneNumber = "", [FromQuery] String email = "")
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        RewardsDB.EditCustomer(id, phoneNumber, email);

        return;
    }

    [HttpPut]
    [Route("{id}/drinks")]
    public void Put(int id, [FromQuery] int numDrinks)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        RewardsDB.UpdateCustomerDrinks(id, numDrinks);

        return;
    }

    [HttpDelete]
    [Route("{id}")]
    public void Delete(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        RewardsDB.RemoveCustomer(id);

        return;
    }
}