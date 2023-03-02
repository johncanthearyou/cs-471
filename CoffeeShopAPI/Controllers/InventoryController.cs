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
    public List<List<String>> Get()
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        List<List<String>> inventory = Database.ViewItems();

        return inventory;
    }

    // [HttpGet]
    // public String Get(int id)
    // {
    //     SqliteConnection sqliteConnection = Database.CreateConnection();
    //     String item = Database.ViewSpecificItem(id);

    //     return item;
    // }

    [HttpPost]
    public void Post(String name, int quantity)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        Database.AddNewItem(name, quantity);

        return;
    }

    [HttpDelete]
    public void Delete(String name)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        Database.RemoveItem(name);

        return;
    }

    [HttpPatch]
    public void Patch(String name, int quantity)
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        Database.UpdateItemQuantity(name, quantity);

        return;
    }
}