using FizzBuzzClass;
namespace FizzBuzzTests;

public class FizzBuzzTests
{
    [Fact]
    public void Print_Should_Return_String()
    {
        var fizzBuzz = new FizzBuzz();

        string output = fizzBuzz.Print();

        Assert.IsType<string>(output.GetType());
    }
}
