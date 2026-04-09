using StringSum;

namespace StringSumTests;

public class UnitTest1
{
    [Fact]
    public void Sum_Returns_String()
    {
        var stringSum = new StringSumClass();
        string first = "5";
        string second = "7";

        var result = stringSum.Sum(first, second);

        Assert.IsType<string>(result);
    }
}
