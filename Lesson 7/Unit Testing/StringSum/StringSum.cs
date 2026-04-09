namespace StringSum;

public class StringSumClass
{
    public string Sum(string num1, string num2)
    {
        int.TryParse(num1, out int n1);
        int.TryParse(num2, out int n2);

        if (n1 < 0) n1 = 0;
        if (n2 < 0) n2 = 0;

        return (n1 + n2).ToString();
    }
}
