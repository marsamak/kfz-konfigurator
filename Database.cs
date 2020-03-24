using System;
using System.Data.Common;
using Npgsql;

class Database
{
    public static Database Instance { get; private set; }
    private NpgsqlConnection Connection { get; set; }

    public DbDataReader Read(string sql)
    {
        return new NpgsqlCommand(sql, Connection).ExecuteReader();
    }

    private Database(string connectionString)
    {
        // Connect to a PostgreSQL database
        Console.WriteLine("Database.connect: " + connectionString);
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