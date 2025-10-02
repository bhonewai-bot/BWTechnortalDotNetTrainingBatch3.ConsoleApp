using TechnortalDotNetTrainingBatch3.ConsoleApp;

/*Student student = new Student(1, "Bhone Wai", new DateTime(2002, 11, 21));
Console.WriteLine($"Birth date {student.DateOfBirth}");
student.StudentId = 1;
student.StudentName = "John Doe";
student.FatherName = "Richard Roe";
student.DateOfBirth = new DateTime(2002, 11, 21);

Student student2 = new Student();
student2.StudentId = 2;
student2.StudentName = "John Doe 2";
student2.FatherName = "Richard Roe 2";

Wallet wallet = new Wallet();
wallet.Name = "Bhone Wai";
wallet.MobileNo = "800932347";
wallet.amount = 6900;
wallet.SetBalance(wallet.amount);


Console.WriteLine(wallet.Balance);*/

string writeText = "Hello World";
File.WriteAllText("file.txt", writeText);

Console.ReadLine();

/*EnumUserType userType = EnumUserType.Guest;
switch (userType)
{
    
    case EnumUserType.Guest:
        break;
    case EnumUserType.User:
        break;
    case EnumUserType.Admin:
        break;
    case EnumUserType.SuperAdmin:
        break;
    case EnumUserType.None:
    default:
        break;
}*/

public abstract class Animal
{
    public abstract void AnimalSound();

    public void Sleep()
    {
        Console.WriteLine("Zzz");
    }
}

public class Cow : Animal
{
    public override void AnimalSound()
    {
        Console.WriteLine("Boob");
    }
}

public interface ITransfer
{
    void Transfer(string fromMobileNo, string toMobileNo, decimal amount);
    void TransactionHistory(string mobileNo);
}

public class Wallet : ITransfer
{
    public void Transfer(string fromMobileNo, string toMobileNo, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void TransactionHistory(string mobileNo)
    { 
        throw new NotImplementedException();
    }
}

public class Bank : ITransfer
{
    public void Transfer(string fromMobileNo, string toMobileNo, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void TransactionHistory(string mobileNo)
    {
        throw new NotImplementedException();
    }
}

public enum EnumUserType
{
    None,
    Guest,
    User,
    Admin,
    SuperAdmin
}