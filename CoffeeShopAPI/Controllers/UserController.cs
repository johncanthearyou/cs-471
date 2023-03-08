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
    public List<Dictionary<string, string>> Get()
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        List<Dictionary<string, string>> users = UserDB.GetAllUsers();

        return users;
    }

    [HttpGet]
    [Route("{username}")]
    public Dictionary<string, string> Get(String username)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        Dictionary<string, string> user = UserDB.GetSingleUser(username);

        return user;
    }

    [HttpPost]
    public void Post([FromQuery] String username, [FromQuery] String password, [FromQuery] String permission)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        UserDB.AddUser(username, password, permission);

        return;
    }

    [HttpDelete]
    [Route("{username}")]
    public void Delete(String username)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        UserDB.RemoveUser(username);

        return;
    }

    [HttpPut]
    [Route("{username}")]
    public void Put(String username, [FromQuery] String password = "", [FromQuery] String permission = "")
    {
        SqliteConnection sqliteConnection = Database.CreateConnection();
        UserDB.UpdateUser(username, password, permission);

        return;
    }
}
