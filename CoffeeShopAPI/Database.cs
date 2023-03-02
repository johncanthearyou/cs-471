using Microsoft.Data.Sqlite;

public static class Database
{
    public static SqliteConnection CreateConnection()
    {
        SqliteConnection sqlite_conn = new SqliteConnection(
            "Data Source=../coffee-shop-db/database.db;"
        );
        sqlite_conn.Open();

        return sqlite_conn;
    }

    public static void ReadData(SqliteConnection conn, String query)
    {
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = query;

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            string myreader = sqlite_datareader.GetString(0);
            Console.WriteLine(myreader);
        }
        conn.Close();
    }

    public static String GetPasswordForUser(String username)
    {
        SqliteConnection conn = CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = $"SELECT password FROM USERS WHERE username = {username}";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        String password = "null";
        if (sqlite_datareader.Read() == true)
        {
            password = sqlite_datareader.GetString(0);
        }
        conn.Close();

        return password;
    }

    public static void AddUser(String username, String password)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        String user_exists = GetPasswordForUser(username);

        if (user_exists == "null")
        {
            sqlite_cmd.CommandText = $"INSERT INTO USERS VALUES ({username}, {password});";
            sqlite_cmd.ExecuteReader();
        }
    }

    public static void RemoveUser(String username)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM USERS WHERE username = {username};";
        sqlite_cmd.ExecuteReader();
    }

    public static List<List<string>> ViewItems()
    {
        SqliteConnection conn = CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM inventory;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<List<String>> inventory = new List<List<string>>();

        while (sqlite_datareader.Read())
        {
            String item = sqlite_datareader.GetString(0);
            String numItems = sqlite_datareader.GetString(1);

            List<String> temp = new List<String>();
            temp.Add(item);
            temp.Add(numItems);

            inventory.Add(temp);
        }

        return inventory;
    }

    public static String ViewSpecificItem(String name)
    {
        SqliteConnection conn = CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM inventory WHERE name = {name};";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        String item = sqlite_datareader.GetString(0);

        return item;
    }

    public static void AddNewItem(String name, int quantity)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO inventory VALUES ({name}, {quantity});";
        sqlite_cmd.ExecuteReader();

        return;
    }

    public static void RemoveItem(String name)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"DELETE FROM inventory WHERE name = {name};";
        sqlite_cmd.ExecuteReader();

        return;
    }

    public static void UpdateItemQuantity(String name, int quantity)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"UPDATE inventory SET quantity = {quantity} WHERE name = {name};";
        sqlite_cmd.ExecuteReader();

        return;
    }

    public static List<List<string>> ViewTransactions()
    {
        SqliteConnection conn = CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM transactions;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<List<string>> transactions = new List<List<string>>();

        while (sqlite_datareader.Read())
        {
            String id = sqlite_datareader.GetString(0);

            List<string> temp = new List<string>();

            temp.Add(id);

            transactions.Add(temp);
        }

        return transactions;
    }

    public static String ViewSpecificTransaction(int id)
    {
        SqliteConnection conn = CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM transactions WHERE id = {id};";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        String transaction = sqlite_datareader.GetString(0);

        return transaction;
    }

    public static void NewTransaction(JsonContent drinks, JsonContent food, String payment, DateTime date, String customerName)
    {
        SqliteConnection conn = CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO transactions VALUES ({drinks}, {food}, {payment}, {date}, {customerName});";
        sqlite_cmd.ExecuteReader();

        return;
    }
}