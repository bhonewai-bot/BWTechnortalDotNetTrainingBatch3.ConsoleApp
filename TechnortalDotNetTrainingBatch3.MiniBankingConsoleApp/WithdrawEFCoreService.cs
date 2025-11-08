using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;
using TechnortalDotNetTrainingBatch3.Shared;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class WithdrawEFCoreService
{
    private readonly AppDbContext _db;

    public WithdrawEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region Withdraw Money
    public void Withdraw()
    {
        string mobileNo = ConsoleInput.GetStringInput(
            "Enter mobile number: ",
            "Mobile number cannot be empty!"
        );
        
        var account = _db.TblAccounts
            .FirstOrDefault(a => a.MobileNo == mobileNo);

        if (account is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (account.Balance < 10000)
        {
            Console.WriteLine("Insufficient balance!");
            return;
        }
        
        decimal amount = ConsoleInput.GetDecimalInput(
            "Enter amount to withdraw: ",
            "Enter valid amount!"
        );
        
        string pin;

        do
        {
            Console.Write("Enter pin number: ");
            pin = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(pin))
            {
                Console.WriteLine("Pin number cannot be empty!");
                continue;
            }

            if (account.Pin != pin)
            {
                Console.WriteLine("Incorrect pin!");
                pin = "";
            }
        } while (string.IsNullOrWhiteSpace(pin));

        account.Balance -= amount;

        var transaction = new TblWithdrawal()
        {
            MobileNo = mobileNo,
            WithdrawalAmount = amount
        };
        
        _db.TblWithdrawals.Add(transaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Withdraw successfully" : "Withdraw failed";
        Console.WriteLine(message);
    }
    #endregion
}