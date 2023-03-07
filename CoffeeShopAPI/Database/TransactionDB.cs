using Microsoft.Data.Sqlite;

public static class TransactionDB
{
    public static List<Dictionary<string, object>> ViewTransactions()
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        List<Dictionary<string, object>> transactions = new List<Dictionary<string, object>>();


        sqlite_cmd.CommandText = $"SELECT id FROM transactions;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();


        while (sqlite_datareader.Read())
        {
            Dictionary<string, object> transaction = new Dictionary<string, object>();

            transaction = ViewSpecificTransaction(Int32.Parse(sqlite_datareader["id"].ToString()));

            transactions.Add(transaction);
        }
        conn.Close();

        return transactions;
    }

    public static Dictionary<string, object> ViewSpecificTransaction(int id)
    {
        SqliteConnection conn = Database.CreateConnection();

        Dictionary<string, object> transaction = new Dictionary<string, object>();
        List<Dictionary<string, object>> dict = new List<Dictionary<string, object>>();


        using (SqliteCommand sqlite_cmd = conn.CreateCommand())
        {
            sqlite_cmd.CommandText = $"SELECT * FROM transactions WHERE id = '{id}';";
            using (SqliteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
            {
                if (sqlite_datareader.Read())
                {
                    transaction["id"] = sqlite_datareader["id"].ToString();
                    transaction["payment"] = sqlite_datareader["payment"].ToString();
                    transaction["totalCost"] = sqlite_datareader["total_cost"].ToString();
                    transaction["date"] = sqlite_datareader["date"].ToString();
                    transaction["customerName"] = sqlite_datareader["customer_name"].ToString();
                    transaction["complete"] = sqlite_datareader["complete"].ToString();
                }
            }
        }

        using (SqliteCommand sqlite_cmd = conn.CreateCommand())
        {
            sqlite_cmd.CommandText = $"SELECT * FROM transactionItems WHERE transaction_id = '{id}';";
            using (SqliteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
            {
                while (sqlite_datareader.Read())
                {
                    Dictionary<string, object> temp = new Dictionary<string, object>();

                    temp["name"] = sqlite_datareader["name"].ToString();
                    temp["quantity"] = sqlite_datareader["quantity"].ToString();

                    dict.Add(temp);
                }

                transaction["item"] = dict;
            }
        }

        conn.Close();

        return transaction;
    }

    public static void NewTransaction(Dictionary<string, int> items, String payment, float totalCost, String customerName)
    {
        SqliteConnection conn = Database.CreateConnection();

        int id;

        using (SqliteCommand sqlite_cmd = conn.CreateCommand())
        {
            sqlite_cmd.CommandText = $"INSERT INTO transactions(payment, total_cost, customer_name) VALUES ('{payment}', '{totalCost}', '{customerName}') RETURNING ID;";
            using (SqliteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
            {
                sqlite_datareader.Read();

                id = sqlite_datareader.GetInt32(0);
            }
        }

        foreach (KeyValuePair<string, int> item in items)
        {
            using (SqliteCommand sqlite_cmd = conn.CreateCommand())
            {
                sqlite_cmd.CommandText = $"INSERT INTO transactionItems(transaction_id, name, quantity) VALUES ('{id}', '{item.Key}', '{item.Value}');";
                sqlite_cmd.ExecuteReader();
            }
        }

        conn.Close();

        return;
    }

    public static void EditTransaction(int id, Object payment, Object customerName)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, object> transaction = ViewSpecificTransaction(id);
        if (payment == null)
        {
            payment = transaction["payment"];
        }
        if (customerName == null)
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