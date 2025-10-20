using System.Data;
using Microsoft.Data.SqlClient;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class SaleService
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
            sale.SaleId,
            sale.ProductId,
            product.ProductName,
            sale.Quantity,
            sale.Price,
            sale.Quantity * sale.Price as TotalAmount
        FROM Tbl_Sale sale
        LEFT JOIN Tbl_Product product ON sale.ProductId = product.ProductId 
        WHERE sale.DeleteFlag = 0";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        
        Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12} {4,-12}", 
            "SaleId", "ProductName", "Quantity", "Price", "TotalAmount");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            int quantity = Convert.ToInt32(row["Quantity"]);
            decimal price = Convert.ToDecimal(row["Price"]);
            decimal totalAmount = Convert.ToDecimal(row["TotalAmount"]);
            
            Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12:N0} {4,-12:N0}", 
                row["SaleId"], row["ProductName"], quantity, price, totalAmount);
        }
        
        connection.Close();
    }

    public void Create()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string insertQuery = @"
        INSERT INTO Tbl_Sale (
            ProductId,
            Quantity,
            Price
        ) 
        SELECT 
            2,
            20,
            500
        FROM Tbl_Product
        WHERE ProductId = 2 AND DeleteFlag = 0 AND Quantity >= 20";

        SqlCommand cmd = new SqlCommand(insertQuery, connection);
        int result = cmd.ExecuteNonQuery();

        if (result > 0)
        {
            string updateQuery = @"
            UPDATE Tbl_Product SET
                Quantity = Quantity - 20,
                ModifiedDateTime = GetDate()
            WHERE ProductId = 2";
            
            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
            updateCmd.ExecuteNonQuery();

            Console.WriteLine("Saving success");
        }
        else
        {
            Console.WriteLine("Saving failed");
        }
        
        connection.Close();
    }
}