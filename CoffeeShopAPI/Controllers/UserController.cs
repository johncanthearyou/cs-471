using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CoffeeShopAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public String Get([FromQuery] String username)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        String password = Database.GetPasswordForUser(username);

        return password;
    }

    [HttpPost]
    public void Post([FromQuery] String username, [FromQuery] String password)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        Database.AddUser(username, password);

        return;
    }

    [HttpDelete]
    public void Delete([FromQuery] String username)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        Database.RemoveUser(username);

        return;
    }
}
