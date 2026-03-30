using System.Configuration;

namespace ConfigurationItemAttributeProject;

public class Program
{
    public static void Main(string[] args)
    {

        var testClass = new SomeClass(){Name = "Unchanged", Age = 23, Weight = 88.5F, WaitingTime = TimeSpan.FromMinutes(30)};

        testClass.SaveSettings();

        testClass.Name = "Changed";
        testClass.Weight += 12.23F;
        testClass.Age -= 3;
        testClass.WaitingTime = TimeSpan.MaxValue;

        testClass.LoadSettings();

        var props = testClass.GetType().GetProperties();
        foreach(var prop in props)
        {
            Console.WriteLine(prop.GetValue(testClass)!.ToString());
        }

    }
}