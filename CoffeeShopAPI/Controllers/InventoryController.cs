using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CoffeeShopAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<Dictionary<string, string>> Get()
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        List<Dictionary<string, string>> inventory = InventoryDB.ViewItems();

        return inventory;
    }

    [HttpGet]
    [Route("{id}")]
    public Dictionary<string, string> Get(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        Dictionary<string, string> item = InventoryDB.ViewSpecificItem(id);

        return item;
    }

    [HttpPost]
    public void Post([FromQuery] String name, [FromQuery] int quantity, [FromQuery] String size, [FromQuery] float price)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        InventoryDB.AddNewItem(name, quantity, size, price);

        return;
    }

    [HttpDelete]
    [Route("{id}")]
    public void Delete(int id)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        InventoryDB.RemoveItem(id);

        return;
    }

    [HttpPut]
    [Route("{id}")]
    public void HttpPut(int id, [FromQuery] int quantity, [FromQuery] float price)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        InventoryDB.UpdateItemQuantity(id, quantity, price);

        return;
    }
}