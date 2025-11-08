using Microsoft.EntityFrameworkCore;
using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class TransactionHistoryEFCoreService
{
    private readonly AppDbContext _db;

    public TransactionHistoryEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region View All Transactions
    public void Read()
    {
        var lts = _db.TblTransactionHistories
            .AsNoTracking()
            .ToList();

        if (lts.Count == 0)
        {
            Console.WriteLine("No transaction history found");
        }

        Console.WriteLine($"{"TransactionID", -20} {"FromMobileNo", -15} {"ToMobileNo", -15} {"Amount", -15} {"TransactionDate", -20}");

        foreach (var item in lts)
        {
            Console.WriteLine($"{item.TransactionHistoryId, -20} {item.FromMobileNo, -15} {item.ToMobileNo, -15} {item.Amount, -15} {item.TransactionDate, -20}");
        }
    }
    #endregion
}