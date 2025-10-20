using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class ProductDapperService
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder =  new SqlConnectionStringBuilder()
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
            
            string query = @"
            SELECT
                ProductId,
                ProductName,
                Quantity,
                Price,
                CreatedDateTime
            FROM Tbl_Product
            WHERE DeleteFlag = 0";

            List<ProductDto> lts = db.Query<ProductDto>(query).ToList();

            foreach (var item in lts)
            {
                Console.WriteLine($"{item.ProductId}.  {item.ProductName} ({item.Price:N0}). createdAt: ({item.CreatedDateTime})");
            }
        }
    }

    public void Create()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            
            string query = @"
            INSERT INTO Tbl_Product (
                ProductName,
                Quantity,
                Price
            ) VALUES (
                'Watermelon',
                20,
                3000
            )";

            int result = db.Execute(query);

            string message = result > 0 ? "Saving successful" : "Saving failed";
            Console.WriteLine(message);
        }
    }

    public void Update()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string query = @"
            UPDATE Tbl_Product SET
                Quantity = Quantity + 20,
                ModifiedDateTime = GetDate()
            WHERE ProductId = 7";

            int result = db.Execute(query);
            
            string message = result > 0 ? "Updating successful" : "Updating failed";
            Console.WriteLine(message);
        }
    }

    public void Delete()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            
            // string query = @"DELETE FROM Tbl_Product WHERE ProductId = 7";
            string query = @"
            UPDATE Tbl_Product SET
                DeleteFlag = 1
            WHERE ProductId = 7";

            int result = db.Execute(query);
            
            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            Console.WriteLine(message);
        }
    }
}

public class ProductDto
{
    public int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public bool DeletedFlag { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    
    public DateTime ModifiedDateTime { get; set; }
}