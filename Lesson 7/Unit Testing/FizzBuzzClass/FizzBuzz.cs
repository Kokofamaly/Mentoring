using System.Text;

namespace FizzBuzzClass;

public class FizzBuzz
{
    public string Print()
    {
        StringBuilder outputSb = new();
        for(int i = 1; i <= 100; i++)
        {
            if(i % 3 == 0 && i % 5 == 0)
            {
                outputSb.AppendLine("FizzBuzz");
            }
            else if(i % 3 == 0)
            {
                outputSb.AppendLine("Fizz");
            }
            else if(i % 5 == 0)
            {
                outputSb.AppendLine("Buzz");
            }
            else
            {
                outputSb.AppendLine(i.ToString());
            }
        }
        string result = outputSb.ToString();
        Console.WriteLine(result);
        return result;
    }
}
