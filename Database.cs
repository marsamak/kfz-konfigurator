using System;
using System.Data.Common;
using Npgsql;

class Database
{
    public static Database Instance { get; private set; }
    private NpgsqlConnection Connection { get; set; }

    public DbDataReader Execute(string sql, params (string, object)[] parameters)
    {
        using (var cmd = new NpgsqlCommand(sql, Connection))
        {
            foreach ((string name, object value) in parameters)
                cmd.Parameters.AddWithValue(name, value);
            return cmd.ExecuteReader();
        }
    }

    private Database(string connectionString)
    {
        // Connect to a PostgreSQL database.
        Connection = new NpgsqlConnection(connectionString);
        Connection.Open();
    }
    public static void connect(string connectionString)
    {
        if (Instance != null)
            throw new InvalidOperationException("already connected");
        Instance = new Database(connectionString);
    }
}