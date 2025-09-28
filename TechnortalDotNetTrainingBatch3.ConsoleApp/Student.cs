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