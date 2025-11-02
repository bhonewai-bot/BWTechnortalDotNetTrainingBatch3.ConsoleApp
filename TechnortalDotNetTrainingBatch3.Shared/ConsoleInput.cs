namespace TechnortalDotNetTrainingBatch3.Shared;

public class ConsoleInput
{
    public static string GetStringInput(string prompt, string? errorMessage)
    {
        string input;
        
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(errorMessage);
            }
        } while (string.IsNullOrEmpty(input));
        
        return input;
    }

    public static int GetIntInput(string prompt, string? errorMessage)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            
            if (int.TryParse(input, out value) && value > 0) break;
            
            Console.WriteLine(errorMessage);
        }
        
        return value;
    }

    public static decimal GetDecimalInput(string prompt, string? errorMessage)
    {
        decimal value;

        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            
            if (decimal.TryParse(input, out value) && value > 0) break;
            
            Console.WriteLine(errorMessage);
        }

        return value;
    }
}