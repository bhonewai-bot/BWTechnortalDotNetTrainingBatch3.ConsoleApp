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

        foreach (var item in lts)
        {
            Console.WriteLine($"{item.CustomerName, -20} {item.MobileNo, -15} {item.Balance, 10:C}");
        }
    }
    #endregion

    #region CreateAccount
    public void Create()
    {
        string customerName = ConsoleInput.GetStringInput(
            "Enter customer name: ",
            "Customer name cannot be empty!"
            );
        
        string mobileNo;

        do
        {
            Console.Write("Enter mobile number: ");
            mobileNo = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(mobileNo))
            {
                Console.WriteLine("Mobile number cannot be empty!");
                continue;
            }

            if (_db.TblAccounts.Any(a => a.MobileNo == mobileNo))
            {
                Console.WriteLine("Phone number already exists. Try another.");
                mobileNo = "";
            }
        } while (string.IsNullOrWhiteSpace(mobileNo));

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

            if (pin.Length != 6 )
            {
                Console.WriteLine("Pin must be 6 digits!");
                pin = "";
            }
        } while (string.IsNullOrWhiteSpace(pin));

        var account = new TblAccount()
        {
            CustomerName = customerName,
            MobileNo = mobileNo,
            Pin = pin
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
        Console.WriteLine($"{account.CustomerName, -20} {account.MobileNo, -15} {account.Balance, 10:C}");
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
        
        string customerName = ConsoleInput.GetStringInput(
                "Enter customer name: ",
                "Customer name cannot be empty!"
            );

        account.CustomerName = customerName;

        _db.Entry(account).State = EntityState.Modified;
        int result = _db.SaveChanges();
        
        string message = result > 0 ? "Account update successfully." : "Account update failed.";
        Console.WriteLine(message);
    }
    #endregion
}