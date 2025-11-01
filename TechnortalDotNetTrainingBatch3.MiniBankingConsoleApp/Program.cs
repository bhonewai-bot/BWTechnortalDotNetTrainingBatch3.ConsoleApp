using TechnortalDotNetTrainingBatch3.MiniBankingConsoleApp;
using TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

using var context = new AppDbContext();
var accountService = new AccountEFCoreService(context);
var transactionService = new TransactionEFCoreService(context);
bool exit = false;

while (!exit)
{
    Console.Clear();
    Console.WriteLine("\n===== Mini Banking System =====");
    Console.WriteLine("Account Management");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. View All Accounts");
    Console.WriteLine("3. View Account Details");
    Console.WriteLine("4. Update Account");
    Console.WriteLine("5. Delete Account");
    Console.WriteLine("\nTransactions");
    Console.WriteLine("6. View All Transactions");
    Console.WriteLine("7. Deposit Money");
    Console.WriteLine("8. Withdraw Money");
    Console.WriteLine("9. Transfer Money");
    Console.WriteLine("0. Exit");
    Console.Write("\nEnter your choice: ");
    
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1": accountService.Create(); return;
        case "2": accountService.Read(); return;
        case "3": accountService.Edit(); return;
        case "4": accountService.Update(); return;
        case "5": accountService.Delete(); return;
        case "6": transactionService.Read(); return;
        case "7": transactionService.Deposit(); return;
        case "8": transactionService.Withdraw(); return;
        case "9": transactionService.Transfer(); return;
        case "0": 
            Console.WriteLine("Exiting...");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            break;
    }
}