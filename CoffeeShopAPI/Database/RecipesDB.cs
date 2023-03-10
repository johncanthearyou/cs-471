using Microsoft.Data.Sqlite;

public static class RecipesDB
{
    public static Dictionary<string, string> ViewRecipe(String name)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM recipe WHERE name = '{name}';";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        Dictionary<string, string> recipe = new Dictionary<string, string>();

        if (sqlite_datareader.Read())
        {
            recipe["id"] = sqlite_datareader["id"].ToString();
            recipe["name"] = sqlite_datareader["name"].ToString();
            recipe["ingredients"] = sqlite_datareader["ingredients"].ToString();
        }

        conn.Close();

        return recipe;       
    }

    public static void AddRecipe(String name, String ingredients)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO recipe(name, ingredients) VALUES ('{name}', '{ingredients}');";
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
