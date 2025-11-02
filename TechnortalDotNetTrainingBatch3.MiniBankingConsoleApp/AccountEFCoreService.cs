using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;
using TechnortalDotNetTrainingBatch3.Shared;

namespace TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;

public class AccountEFCoreService
{
    private readonly AppDbContext _db;

    public AccountEFCoreService(AppDbContext db)
    {
        _db = db;
    }

    #region View All Accounts
    public void Read()
    {
        var lts = _db.TblAccounts
            .AsNoTracking()
            .ToList();

        if (lts.Count == 0)
        {
            Console.WriteLine("No data found!");
            return;
        }
        
        Console.WriteLine($"{"Name",-20} {"Phone",-15} {"Balance",10}");

        foreach (var account in lts)
        {
            Console.WriteLine($"{account.AccountName, -20} {account.PhNumber, -15} {account.Balance, 10:C}");
        }
    }
    #endregion

    #region CreateAccount
    public void Create()
    {
        string accountName = ConsoleInput.GetStringInput(
            "Enter account name: ",
            "Account name cannot be empty!"
            );
        string phNumber;

        do
        {
            Console.Write("Enter phone number: ");
            phNumber = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(phNumber))
            {
                Console.WriteLine("Phone number cannot be empty!");
                continue;
            }

            if (_db.TblAccounts.Any(account => account.PhNumber == phNumber))
            {
                Console.WriteLine("Phone number already exists. Try another.");
                phNumber = "";
            }
        } while (string.IsNullOrWhiteSpace(phNumber));

        var account = new TblAccount()
        {
            AccountName = accountName,
            PhNumber = phNumber
        };

        _db.Add(account);
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Account create successfully." : "Account create failed.";
        Console.WriteLine(message);
    }
    #endregion

    #region View Account Details
    public void Edit()
    {
        int accountId = ConsoleInput.GetIntInput(
                "Enter account ID: ",
                "Account ID cannot be empty!"
            );

        var account = _db.TblAccounts
            .AsNoTracking()
            .FirstOrDefault(account => account.AccountId == accountId);

        if (account is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }

        Console.WriteLine($"{"Name",-20} {"Phone",-15} {"Balance",10}");
        Console.WriteLine($"{account.AccountName, -20} {account.PhNumber, -15} {account.Balance, 10:C}");
    }
    #endregion

    #region Update Account
    public void Update()
    {
        int accountId = ConsoleInput.GetIntInput(
            "Enter account ID: ",
            "Account ID cannot be empty!");
        
        var account = _db.TblAccounts
            .AsNoTracking()
            .FirstOrDefault(account => account.AccountId == accountId);

        if (account is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }
        
        string accountName = ConsoleInput.GetStringInput(
                "Enter account name: ",
                "Account name cannot be empty!"
            );

        account.AccountName = accountName;

        _db.Entry(account).State = EntityState.Modified;
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Account update successfully." : "Account update failed.";
        Console.WriteLine(message);
    }
    #endregion

    #region Delete Account
    public void Delete()
    {
        
    }
    #endregion
}