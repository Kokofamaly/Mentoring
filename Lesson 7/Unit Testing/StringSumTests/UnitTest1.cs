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

    [Fact]
    public void Sum_Sums_Natural_Numbers()
    {
        var stringSum = new StringSumClass();
        string first = "5";
        string second = "7";
        string expected = "12";
        
        var result = stringSum.Sum(first, second);
    
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Sum_Of_NegativeNumbers_Returns_Zero()
    {
        var stringSum = new StringSumClass();
        string first = "-5";
        string second = "-7";
        string expected = "0";
    
        var result = stringSum.Sum(first, second);
    
        Assert.Equal(expected, result);
    }


}
