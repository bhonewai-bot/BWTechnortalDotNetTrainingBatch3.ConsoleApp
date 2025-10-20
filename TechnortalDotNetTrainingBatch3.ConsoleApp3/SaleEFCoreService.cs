using TechnortalDotNetTrainingBatch3.ConsoleApp3.Database.AppDbContextModels;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class SaleEFCoreService
{
    private readonly AppDbContext _db;

    public SaleEFCoreService()
    {
        _db = new AppDbContext();
    }

    public void Read()
    {
        var lts = _db.TblSales
            .Where(s => s.DeleteFlag == false)
            .Select(s => new
            {
                s.SaleId,
                ProductName = s.Product.ProductName,
                s.Quantity,
                s.Price,
                TotalAmount = s.Quantity * s.Price
            })
            .ToList();
        
        Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12} {4,-12}", 
            "SaleId", "ProductName", "Quantity", "Price", "TotalAmount");

        foreach (var item in lts)
        {
            Console.WriteLine("{0,-8} {1,-20} {2,-10} {3,-12:N0} {4,-12:N0}", 
                item.SaleId, item.ProductName, item.Quantity, item.Price, item.TotalAmount);
        }
    }

    public void Create()
    {
        int productId = 8;
        int quantity = 15;
        decimal price = 1500m;

        var product = _db.TblProducts
            .FirstOrDefault(p => p.ProductId == productId && p.DeleteFlag == false && p.Quantity >= quantity);

        if (product is null)
        {
            return;
        }

        var sale = new TblSale
        {
            ProductId = productId,
            Quantity = quantity,
            Price = price,
        };

        _db.Add(sale);

        product.Quantity -= quantity;
        product.ModifiedDateTime = DateTime.Now;
        
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Saving successful" : "Saving failed";
        Console.WriteLine(message);
    }
}