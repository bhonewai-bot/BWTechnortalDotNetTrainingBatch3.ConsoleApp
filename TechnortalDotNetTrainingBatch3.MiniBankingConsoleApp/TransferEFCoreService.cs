using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;
using TechnortalDotNetTrainingBatch3.Shared;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class TransferEFCoreService
{
    private readonly AppDbContext _db;

    public TransferEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region Transfer Money

    public void Transfer()
    {
        decimal amount;
        
        string mobileNo = ConsoleInput.GetStringInput(
            "Enter sender mobile number: ",
            "Mobile number cannot be empty!"
        );
        
        var fromAccount = _db.TblAccounts.FirstOrDefault(a => a.MobileNo == mobileNo);
        if (fromAccount is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }
        
        if (fromAccount.Balance <= 0)
        {
            Console.WriteLine("Insufficient balance");
            return;
        }
        
        string mobileNo2 = ConsoleInput.GetStringInput(
            "Enter receiver mobile number: ",
            "Mobile number cannot be empty!"
        );
        
        var toAccount = _db.TblAccounts.FirstOrDefault(a => a.MobileNo == mobileNo2);
        if (toAccount is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (fromAccount == toAccount)
        {
            Console.WriteLine("You cannot transfer money to the same account.");
            return;
        }

        while (true)
        {
            Console.Write("Enter amount to transfer: ");
            string input = Console.ReadLine() ?? "";
            
            if (!decimal.TryParse(input, out amount) || amount <= 0)
            {
                Console.WriteLine("Enter valid amount!");
                continue;
            }

            if (amount > fromAccount?.Balance)
            {
                Console.WriteLine("You can't transfer more than the balance!");
                continue;
            }
            
            break;
        }
        
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

            if (fromAccount.Pin != pin)
            {
                Console.WriteLine("Incorrect pin!");
                pin = "";
            }
        } while (string.IsNullOrWhiteSpace(pin));
        
        fromAccount!.Balance -= amount;
        toAccount.Balance += amount;

        var transaction = new TblTransactionHistory()
        {
            FromMobileNo = fromAccount.MobileNo,
            ToMobileNo = toAccount.MobileNo,
            Amount = amount,
        };
        
        _db.TblTransactionHistories.Add(transaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Transfer successfully" : "Transfer failed";
        Console.WriteLine(message);
    }

    #endregion
}