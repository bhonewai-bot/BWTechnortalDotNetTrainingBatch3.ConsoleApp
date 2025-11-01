using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TechnortalDotNetTrainingBatch3.MiniBankingDatabase;
using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class TransactionEFCoreService
{
    private readonly AppDbContext _db;

    public TransactionEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region View All Transactions
    public void Read()
    {
        var lts = _db.TblTransactions
            .AsNoTracking()
            .ToList();

        if (lts.Count == 0)
        {
            Console.WriteLine("No data found!");
            return;
        }

        Console.WriteLine($"{"TransactionID", -20} {"AccountID", -15} {"Type", -15} {"Amount", -15} {"CreatedAt", -20}");

        foreach (var transaction in lts)
        {
            Console.WriteLine($"{transaction.TransactionId, -20} {transaction.AccountId, -15} {transaction.Type,-15} {transaction.Amount,-15:C} {transaction.CreatedAt}");
        }
    }
    #endregion

    #region Deposit Money
    public void Deposit()
    {
        string phNumber;
        decimal amount;

        do
        {
            Console.Write("Enter phone number: ");
            phNumber = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(phNumber))
            {
                Console.WriteLine("Phone number cannot be empty!");
            }
        } while (string.IsNullOrWhiteSpace(phNumber));

        var account = _db.TblAccounts
            .FirstOrDefault(account => account.PhNumber == phNumber);
        
        if (account is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        while (true)
        {
            Console.Write("Enter amount to deposit: ");
            string input = Console.ReadLine() ?? "";
            
            if (!decimal.TryParse(input, out amount) || amount <= 0 )
            {
                Console.WriteLine("Enter valid amount!");
                continue;
            };

            break;
        }

        account.Balance += amount;

        var transaction = new TblTransaction()
        {
            AccountId = account.AccountId,
            Type = TransactionType.Deposit,
            Amount = amount
        };
        
        _db.Add(transaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Deposit successfully." : "Deposit failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Withdraw Money
    public void Withdraw()
    {
        string phNumber;
        decimal amount;
        
        do
        {
            Console.Write("Enter phone number: ");
            phNumber = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(phNumber))
            {
                Console.WriteLine("Phone number cannot be empty!");
            }
        } while (string.IsNullOrWhiteSpace(phNumber));

        var account = _db.TblAccounts
            .FirstOrDefault(account => account.PhNumber == phNumber);

        if (account is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (account.Balance == 0)
        {
            Console.WriteLine("Account balance is zero. Withdrawal not possible.");
            return;
        }

        while (true)
        {
            Console.Write("Enter amount to withdraw: ");
            string input = Console.ReadLine() ?? "";
            
            if (!decimal.TryParse(input, out amount) || amount <= 0)
            {
                Console.WriteLine("Enter valid amount!");
                continue;
            };

            if (amount > account.Balance)
            {
                Console.WriteLine("Insufficient balance!");
                continue;
            }

            break;
        }

        account.Balance -= amount;

        var transaction = new TblTransaction()
        {
            AccountId = account.AccountId,
            Type = TransactionType.Withdraw,
            Amount = amount
        };

        _db.Add(transaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Withdraw successfully." : "Withdraw failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Transfer Money
    public void Transfer()
    {
        int accountId;
        string phNumber;
        decimal amount;

        while (true)
        {
            Console.Write("Enter sender ID: ");
            string input = Console.ReadLine() ?? "";
            
            if (int.TryParse(input, out accountId) && accountId > 0) break;

            Console.WriteLine("Enter valid ID!");
        }
        
        var sender = _db.TblAccounts
            .FirstOrDefault(account => account.AccountId == accountId);

        if (sender is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (sender.Balance == 0)
        {
            Console.WriteLine("Insufficient balance!");
            return;
        }
        
        do
        {
            Console.Write("Enter receiver phone number: ");
            phNumber = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(phNumber))
            {
                Console.WriteLine("Phone number cannot be empty!");
            }
        } while (string.IsNullOrWhiteSpace(phNumber));
        
        var receiver = _db.TblAccounts
            .FirstOrDefault(account => account.PhNumber == phNumber);
        
        if (receiver is null)
        {
            Console.WriteLine("Account not found!");
            return;
        }

        if (receiver.AccountId == sender.AccountId)
        {
            Console.WriteLine("You cannot transfer money to your own account!");
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

            if (amount > sender?.Balance)
            {
                Console.WriteLine("Insufficient balance!");
                continue;
            }
            
            break;
        }

        sender.Balance -= amount;
        receiver.Balance += amount;

        var withdrawTransaction = new TblTransaction()
        {
            AccountId = sender.AccountId,
            Type = TransactionType.Transfer,
            Amount = -amount
        };

        var depositTransaction = new TblTransaction()
        {
            AccountId = receiver.AccountId,
            Type = TransactionType.Transfer,
            Amount = amount,
        };
        
        _db.Add(withdrawTransaction);
        _db.Add(depositTransaction);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Transfer successfully." : "Transfer failed";
        Console.WriteLine(message);
    }
    #endregion
}