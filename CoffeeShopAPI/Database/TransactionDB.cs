using Microsoft.Data.Sqlite;

public static class TransactionDB
{
    public static List<Dictionary<string, string>> ViewTransactions()
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM transactions;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<Dictionary<string, string>> transactions = new List<Dictionary<string, string>>();

        while (sqlite_datareader.Read())
        {
            Dictionary<string, string> transaction = new Dictionary<string, string>();

            transaction["id"] = sqlite_datareader["id"].ToString();
            transaction["drinks"] = sqlite_datareader["drinks"].ToString();
            transaction["food"] = sqlite_datareader["food"].ToString();
            transaction["payment"] = sqlite_datareader["payment"].ToString();
            transaction["totalCost"] = sqlite_datareader["total_cost"].ToString();
            transaction["date"] = sqlite_datareader["date"].ToString();
            transaction["customerName"] = sqlite_datareader["customer_name"].ToString();
            transaction["complete"] = sqlite_datareader["complete"].ToString();

            transactions.Add(transaction);
        }
        conn.Close();

        return transactions;
    }

    public static Dictionary<string, string> ViewSpecificTransaction(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM transactions WHERE id = '{id}';";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        Dictionary<string, string> transaction = new Dictionary<string, string>();

        if (sqlite_datareader.Read())
        {
            transaction["id"] = sqlite_datareader["id"].ToString();
            transaction["drinks"] = sqlite_datareader["drinks"].ToString();
            transaction["food"] = sqlite_datareader["food"].ToString();
            transaction["payment"] = sqlite_datareader["payment"].ToString();
            transaction["totalCost"] = sqlite_datareader["total_cost"].ToString();
            transaction["date"] = sqlite_datareader["date"].ToString();
            transaction["customerName"] = sqlite_datareader["customer_name"].ToString();
            transaction["complete"] = sqlite_datareader["complete"].ToString();
        }

        conn.Close();

        return transaction;
    }

    public static void NewTransaction(String payment, float totalCost, String customerName)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"INSERT INTO transactions(payment, total_cost, customer_name) VALUES ('{payment}', '{totalCost}', '{customerName}');";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void EditTransaction(int id, String payment, String customerName)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> transaction = ViewSpecificTransaction(id);
        if (payment == "")
        {
            payment = transaction["payment"];
        }
        if (customerName == "")
        {
            customerName = transaction["customerName"];
        }

        sqlite_cmd.CommandText = $"UPDATE transactions SET payment = '{payment}', customer_name = '{customerName}' WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void MarkTransactionComplete(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"UPDATE transactions SET complete = 1 WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void RemoveTransaction(int id)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"DELETE FROM transactions WHERE id = '{id}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }
}