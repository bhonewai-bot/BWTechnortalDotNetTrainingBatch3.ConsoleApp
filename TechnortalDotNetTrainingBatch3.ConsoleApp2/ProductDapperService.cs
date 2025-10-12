using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp2;

public class ProductDapperService
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
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            
            string query = @"SELECT
                ProductId, ProductName, Quantity, Price, DeleteFlag
            FROM Tbl_Product
            WHERE DeleteFlag = 0";

            var lts = db.Query<ProductDto>(query).ToList();

            for (int i = 0; i < lts.Count; i++)
            {
                Console.WriteLine(lts[i].ProductId);
                Console.WriteLine(lts[i].ProductName);
            }
        }
    }

    public void Create()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string query = @"INSERT INTO Tbl_Product (
                ProductName, 
                Quantity, 
                Price, 
                DeleteFlag
            ) VALUES (
                'Lemon',
                30,
                500,
                0
            )";

            int result = db.Execute(query);
            
            string message = result > 0 ? "Saving successful." : "Saving failed.";
            Console.WriteLine(message);
        }
    }

    public void Update()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string query = @"UPDATE Tbl_Product SET
                ProductName = 'Kiwi',
                Price = 3500
            WHERE ProductId = 1006";

            int result = db.Execute(query);
            
            string message = result > 0 ? "Updated successful." : "Updated failed.";
            Console.WriteLine(message);
        }
    }

    public void Delete()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            
            string query = @"DELETE FROM Tbl_Product WHERE ProductId = 1002";

            int result = db.Execute(query);
            
            string message = result > 0 ? "Deleted successful." : "Deleted failed.";
            Console.WriteLine(message);
        }
    }
}