using TechnortalDotNetTrainingBatch3.ConsoleApp3.Database.AppDbContextModels;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp3;

public class ProductEFCoreService
{
    private readonly AppDbContext _db;

    public ProductEFCoreService()
    {
        _db = new AppDbContext();
    }

    public void Read()
    {
        var lts = _db.TblProducts.Where(x => x.DeleteFlag == false).ToList();

        foreach (var item in lts)
        {
            Console.WriteLine($"{item.ProductId}.  {item.ProductName} ({item.Price:N0}). createdAt: ({item.CreatedDateTime})");
        }
    }

    public void Create()
    {
        var item = new TblProduct()
        {
            ProductName = "Avocado",
            Quantity = 50,
            Price = 1500
        };

        _db.Add(item);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Saving successful" : "Saving failed";
        Console.WriteLine(message);
    }

    public void Update()
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == 3);
        if (item is null)
        {
            return; 
        }

        item.Quantity += 15;
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Updating successful" : "Updating failed";
        Console.WriteLine(message);
    }

    public void Delete()
    {
        var item = _db.TblProducts.FirstOrDefault(x => x.ProductId == 8);
        if (item is null)
        {
            return;
        }
        
        item.DeleteFlag = true;
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Deleting successful" : "Deleting failed";
        Console.WriteLine(message);
    }
}