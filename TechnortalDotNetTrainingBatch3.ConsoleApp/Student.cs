namespace TechnortalDotNetTrainingBatch3.ConsoleApp;

internal class Student : Father 
{
    public Student(int studentNo, string studentName, DateTime dateOfBirth)
    {
        StudentNo = studentNo;
        StudentName = studentName;
        DateOfBirth = dateOfBirth;
    }
    
    // Properties
    public int StudentNo { get; set; }
    public string StudentName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class Father
{
    protected string ProName;

    protected void Punch()
    {
        Console.WriteLine("Punch");
    }
}

public class Employee
{
    public string Name;
    
    public int EmployeeNo { get; set; }
    
    public string EmployeeName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class Wallet
{
    public decimal amount;
    public string Name { get; set; }
    
    public string MobileNo { get; set; }

    public decimal Balance { get; private set; }

    public decimal ShowBalance()
    {
        return Balance;
    }

    public void SetBalance(decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception("Amount must be greater than 0");
        }

        Balance = amount;
    }
}