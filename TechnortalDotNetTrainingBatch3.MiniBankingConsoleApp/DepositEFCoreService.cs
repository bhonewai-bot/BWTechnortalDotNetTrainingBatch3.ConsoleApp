using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;
using TechnortalDotNetTrainingBatch3.Shared;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class DepositEFCoreService
{
    private readonly AppDbContext _db;

    public DepositEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region Deposit Money
    public void Deposit()
    {
        string mobileNo = ConsoleInput.GetStringInput(
            "Enter mobile number: ",
            "Mobile number cannot be empty!"
        );

        var account = _db.TblAccounts
            .FirstOrDefault(a => a.MobileNo == mobileNo);

        if (account is null)
        {
            Console.WriteLine("Account not found");
            return;
        }

        decimal amount = ConsoleInput.GetDecimalInput(
            "Enter amount to deposit: ",
            "Enter valid amount!"
        );

        account.Balance += amount;

        var transaction = new TblDeposit()
        {
            MobileNo = mobileNo,
            DepositAmount = amount
        };

        _db.TblDeposits.Add(transaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Deposit successfully" : "Deposit failed";
        Console.WriteLine(message);
    }
    #endregion
}