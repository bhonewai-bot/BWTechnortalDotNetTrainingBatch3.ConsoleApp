using TechnortalDotNetTrainingBatch3.ConsoleApp2.Database.AppDbContextModels;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp2;

public class ProductCategoryService
{

    public void Read()
    {
        AppDbContext db = new AppDbContext();
        var lts = db.TblProductCategories.ToList();
    }
}