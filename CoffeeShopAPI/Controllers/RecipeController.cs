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
    public List<Dictionary<string, string>> Get()
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        List<Dictionary<string, string>> recipe = RecipesDB.ViewAllRecipes();

        return recipe;
    }

    [HttpPost]
    public void Post([FromQuery] String name, [FromQuery] float price, [FromQuery] String ingredients)
    {
        SqliteConnection sqlConnection = Database.CreateConnection();
        RecipesDB.AddRecipe(name, price, ingredients);

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