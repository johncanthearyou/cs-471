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
}