using System.Text;

namespace FizzBuzzClass;

public class FizzBuzz
{
    public string GetValue(int number)
    {
        if(number % 3 == 0 && number % 5 == 0) return "FizzBuzz";
        else if(number % 3 == 0) return "Fizz";
        else if(number % 5 == 0) return "Buzz";
        return number.ToString();
    }
    public string Print()
    {
        StringBuilder outputSb = new();
        for(int i = 1; i <= 100; i++)
        {
            outputSb.AppendLine(GetValue(i));
        }
        string result = outputSb.ToString();
        Console.WriteLine(result);
        return result;
    }
}
