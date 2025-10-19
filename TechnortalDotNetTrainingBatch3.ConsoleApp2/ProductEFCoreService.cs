namespace TechnortalDotNetTrainingBatch3.ConsoleApp2;

public class ProductEFCoreService
{
    private readonly ModelFirstAppDbContext _db;

    public ProductEFCoreService()
    {
        _db = new ModelFirstAppDbContext();
    }
    
    public void Read()
    {
        var lts = _db.Products.Where(x => x.DeleteFlag == false).ToList();

        for (int i = 0; i < lts.Count; i++)
        {
            Console.WriteLine(lts[i].ProductId);
            Console.WriteLine(lts[i].ProductName);
        }
    }

    public void Create()
    {
        var item = new Tbl_Product()
        {
            ProductName = "Papaya",
            Quantity = 20,
            Price = 4500,
            DeleteFlag = false,
        };
        
        _db.Add(item);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Saving successful" : "Saving failed";
        Console.WriteLine(message);
    }

    public void Update()
    {
        var item = _db.Products.FirstOrDefault(x => x.ProductId == 1006);
        if (item is null)
        {
            return;
        }
        
        item.ProductName = "Watermelon";
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Updating successful" : "Updating failed";
        Console.WriteLine(message);
    }
    
    public void Delete()
    {
        var item = _db.Products.FirstOrDefault(x => x.ProductId == 1006);
        if (item is null)
        {
            return;
        }
        
        // _db.Remove(item);
        item.DeleteFlag = true;
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Deleting successful" : "Deleting failed";
        Console.WriteLine(message);
    }
}