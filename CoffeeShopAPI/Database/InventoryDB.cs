using Microsoft.Data.Sqlite;

public static class InventoryDB
{
    public static List<Dictionary<string, string>> ViewItems()
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM inventory;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<Dictionary<string, string>> inventory = new List<Dictionary<string, string>>();

        while (sqlite_datareader.Read())
        {
            Dictionary<string, string> item = new Dictionary<string, string>();

            item["id"] = sqlite_datareader["id"].ToString();
            item["name"] = sqlite_datareader["name"].ToString();
            item["quantity"] = sqlite_datareader["quantity"].ToString();
            item["size"] = sqlite_datareader["size"].ToString();
            item["price"] = sqlite_datareader["price"].ToString();

            inventory.Add(item);
        }

        conn.Close();

        return inventory;
    }

    public static Dictionary<string, string> ViewSpecificItem(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM inventory WHERE id = {id};";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        Dictionary<string, string> item = new Dictionary<string, string>();

        if (sqlite_datareader.Read())
        {
            item["id"] = sqlite_datareader["id"].ToString();
            item["name"] = sqlite_datareader["name"].ToString();
            item["quantity"] = sqlite_datareader["quantity"].ToString();
            item["size"] = sqlite_datareader["size"].ToString();
            item["price"] = sqlite_datareader["price"].ToString();
        }

        conn.Close();

        return item;
    }

    public static void AddNewItem(String name, int quantity, String size, float price)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO inventory(name, quantity, size, price) VALUES ('{name}', '{quantity}', '{size}', '{price}');";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void RemoveItem(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"DELETE FROM inventory WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void UpdateItemQuantity(int id, int quantity = 0, float price = 0)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> item = ViewSpecificItem(id);

        if (quantity == 0)
        {
            quantity = Int32.Parse(item["quantity"]);
        }
        if (price == 0)
        {
            price = float.Parse(item["price"]);
        }

        sqlite_cmd.CommandText = $"UPDATE inventory SET quantity = '{quantity}', price = '{price}' WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }
}