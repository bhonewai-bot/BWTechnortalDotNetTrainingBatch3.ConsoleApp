using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class SaleDapperService
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

            string query = @"
            SELECT
                sale.SaleId,
                product.ProductName,
                sale.Quantity,
                sale.Price,
                sale.Quantity * sale.Price as TotalAmount
            FROM Tbl_Sale sale
            LEFT JOIN Tbl_Product product ON sale.ProductId = product.ProductId
            WHERE sale.DeleteFlag = 0";

            List<SaleDto> lts = db.Query<SaleDto>(query).ToList();
            
            Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12} {4,-12}", 
                "SaleId", "ProductName", "Quantity", "Price", "TotalAmount");

            foreach (var item in lts)
            {
                Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12:N0} {4,-12:N0}", 
                    item.SaleId, item.ProductName, item.Quantity, item.Price, item.TotalAmount);
            }
        }
    }

    public void Create()
    {
        using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string insertQuery = @"
            INSERT INTO Tbl_Sale (
                ProductId,
                Quantity,
                Price
            )
            SELECT
                2,
                30,
                300
            FROM Tbl_Product
            WHERE ProductId = 2 AND DeleteFlag = 0 AND Quantity >= 30";

            int result = db.Execute(insertQuery);

            if (result > 0)
            {
                string updateQuery = @"
                UPDATE Tbl_Product SET
                    Quantity = Quantity - 30,
                    ModifiedDateTime = GetDate()
                WHERE ProductId = 2";
                
                db.Execute(updateQuery);

                Console.WriteLine("Saving success");
            }
            else
            {
                Console.WriteLine("Saving failed");
            }
        }
    }
}

public class SaleDto
{
    public int SaleId { get; set; }
    
    public int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public bool DeleteFlag { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
}