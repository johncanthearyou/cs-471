using Microsoft.Data.Sqlite;

public static class UserDB
{
    public static List<Dictionary<string, string>> GetAllUsers()
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM users;";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();

        while (sqlite_datareader.Read() == true)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();

            user["username"] = sqlite_datareader["username"].ToString();
            user["password"] = sqlite_datareader["password"].ToString();
            user["permission"] = sqlite_datareader["permission"].ToString();

            users.Add(user);
        }

        conn.Close();

        return users;
    }

    public static Dictionary<string, string> GetSingleUser(String username)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        sqlite_cmd.CommandText = $"SELECT * FROM USERS WHERE username = '{username}'";
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        Dictionary<string, string> user = new Dictionary<string, string>();

        if (sqlite_datareader.Read())
        {
            user["username"] = sqlite_datareader["username"].ToString();
            user["password"] = sqlite_datareader["password"].ToString();
            user["permission"] = sqlite_datareader["permission"].ToString();
        }

        conn.Close();

        return user;
    }

    public static void AddUser(String username, String password, String permission)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> user = GetSingleUser(username);
        if (!user.Values.Any())
        {
            sqlite_cmd.CommandText = $"INSERT INTO users VALUES ('{username}', '{password}', '{permission}');";
            sqlite_cmd.ExecuteReader();
        }
        conn.Close();

        return;
    }

    public static void RemoveUser(String username)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM USERS WHERE username = '{username}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }

    public static void UpdateUser(String username, String password, String permission)
    {
        SqliteConnection conn = Database.CreateConnection();
        SqliteCommand sqlite_cmd;
        sqlite_cmd = conn.CreateCommand();

        Dictionary<string, string> user = GetSingleUser(username);

        if (password == "")
        {
            password = user["password"];
        }
        if (permission == "")
        {
            permission = user["permission"];
        }

        sqlite_cmd.CommandText = $"UPDATE users SET password = '{password}', permission = '{permission}' WHERE username = '{username}';";
        sqlite_cmd.ExecuteReader();

        conn.Close();

        return;
    }
}