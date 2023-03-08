using Microsoft.Data.Sqlite;

public static class RewardsDB
{
    public static Dictionary<string, string> FindCustomer(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM rewards WHERE id = '{id}';";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        Dictionary<string, string> customer = new Dictionary<string, string>();

        if (sqlite_datareader.Read())
        {
            customer["id"] = sqlite_datareader["id"].ToString();
            customer["customerName"] = sqlite_datareader["customer_name"].ToString();
            customer["phoneNumber"] = sqlite_datareader["phone_number"].ToString();
            customer["email"] = sqlite_datareader["email"].ToString();
            customer["drinksUntilFree"] = sqlite_datareader["drinks_until_free"].ToString();
        }

        conn.Close();

        return customer;
    }

    public static void AddNewCustomer(String customerName, String phoneNumber, String email)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO rewards(customer_name, phone_number, email) VALUES ('{customerName}', '{phoneNumber}', '{email}');";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void EditCustomer(int id, String phoneNumber, String email)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> customer = FindCustomer(id);

        if (phoneNumber == "")
        {
            phoneNumber = customer["phoneNumber"];
        }
        if (email == "")
        {
            email = customer["email"];
        }

        sqlite_cmd.CommandText = $"UPDATE rewards SET phone_number = '{phoneNumber}', email = '{email}' WHERE id = '{id}' ;";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void UpdateCustomerDrinks(int id, int numDrinks)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> customer = FindCustomer(id);

        sqlite_cmd.CommandText = $"UPDATE rewards SET drinks_until_free = '{Int32.Parse(customer["drinksUntilFree"]) - numDrinks}' WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void RemoveCustomer(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"DELETE FROM rewards WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }
}