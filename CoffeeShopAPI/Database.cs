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
}