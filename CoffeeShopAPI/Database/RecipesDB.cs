using Microsoft.Data.Sqlite;

public static class RecipesDB
{
    public static List<Dictionary<string, string>> ViewAllRecipes()
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM recipe;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<Dictionary<string, string>> inventory = new List<Dictionary<string, string>>();

        while (sqlite_datareader.Read())
        {
            Dictionary<string, string> recipe = new Dictionary<string, string>();

            recipe["id"] = sqlite_datareader["id"].ToString();
            recipe["name"] = sqlite_datareader["name"].ToString();
            recipe["price"] = sqlite_datareader["price"].ToString();
            recipe["ingredients"] = sqlite_datareader["ingredients"].ToString();

            inventory.Add(recipe);
        }

        conn.Close();

        return inventory;       
    }

    public static void AddRecipe(String name, float price, String ingredients)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO recipe(name, price, ingredients) VALUES ('{name}', '{price}', '{ingredients}');";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void RemoveRecipe(String name)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"DELETE FROM recipe WHERE name = '{name}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }
}
