using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CoffeeShopAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{name}")]
    public Dictionary<string, string> Get(String name)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        Dictionary<string, string> recipe = RecipesDB.ViewRecipe(name);

        return recipe;
    }

    [HttpPost]
    public void Post([FromQuery] String name, [FromQuery] String ingredients)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        RecipesDB.AddRecipe(name, ingredients);

        return;
    }

    [HttpDelete]
    [Route("{name}")]
    public void Delete(String name)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        RecipesDB.RemoveRecipe(name);

        return;
    }
}