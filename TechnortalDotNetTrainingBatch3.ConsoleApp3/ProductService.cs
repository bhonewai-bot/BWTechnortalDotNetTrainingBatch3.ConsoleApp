using System.Data;
using Microsoft.Data.SqlClient;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class ProductService
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "MiniPOS",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    public void Read()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"
        SELECT 
            ProductId, 
            ProductName, 
            Quantity,
            Price,
            DeleteFlag,
            CreatedDateTime,
            ModifiedDateTime
        FROM Tbl_Product
        WHERE DeleteFlag = 0";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];

            decimal price = Convert.ToDecimal(row["Price"]);
            Console.WriteLine($"{row["ProductId"]}. {row["ProductName"]} ({price:N0})");
        }
        
        connection.Close();
    }

    public void Create()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"
        INSERT INTO Tbl_Product (
            ProductName,
            Quantity,
            Price
        ) VALUES (
            'test',
            100,
            2000
        )";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();
        
        connection.Close();
        
        string message = result > 0 ? "Saving successful" : "Saving failed";
        Console.WriteLine(message);
    }

    public void Update()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"
        UPDATE Tbl_Product SET
            Quantity = 30,
            ModifiedDateTime = GetDate()
        WHERE ProductId = 1";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();
        
        connection.Close();
        
        string message = result > 0 ? "Updating successful" : "Updating failed";
        Console.WriteLine(message);
    }

    public void Delete()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        
        string query = @"DELETE FROM Tbl_Product WHERE ProductId = 6";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();
        
        connection.Close();
        
        string message = result > 0 ? "Deleting successful" : "Deleting failed";
        Console.WriteLine(message);
    }
}