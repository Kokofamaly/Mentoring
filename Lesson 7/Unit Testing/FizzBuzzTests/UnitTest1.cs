using FizzBuzzClass;
namespace FizzBuzzTests;

public class FizzBuzzTests
{
    [Fact]
    public void Print_Returns_String()
    {
        var fizzBuzz = new FizzBuzz();

        string output = fizzBuzz.Print();

        Assert.IsType<string>(output);
    }

    [Fact]
    public void Print_Number_1_Returns_1()
    {
        var fizzBuzz = new FizzBuzz();
        string output = fizzBuzz.Print();
        Assert.Contains("1", output);
    }

    [Fact]
    public void Print_Number_3_Returns_Fizz()
    {
        var fizzBuzz = new FizzBuzz();
        string output = fizzBuzz.Print();
        Assert.Contains("Fizz", output);
    }

    [Fact]
    public void Print_Number_5_Returns_Buzz()
    {
        var fizzBuzz = new FizzBuzz();
        string output = fizzBuzz.Print();
        Assert.Contains("Buzz", output);
    }

    [Fact]
    public void Print_Number_15_Returns_FizzBuzz()
    {
        var fizzBuzz = new FizzBuzz();
        string output = fizzBuzz.Print();
        Assert.Contains("FizzBuzz", output);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    [InlineData(30, "FizzBuzz")]
    [InlineData(100, "Buzz")]
    public void GetValue_Should_Return_Correct_FizzBuzz_For_Number(int number, string expected)
    {
        var fizzBuzz = new FizzBuzz();

        var result = fizzBuzz.GetValue(number);
        
        Assert.Equal(expected, result);
    }
    
}
