using System.Configuration;

namespace ConfigurationItemAttributeProject;

public class Program
{
    public static void Main(string[] args)
    {


        // string pathSettingsTxt = "/Users/kokofamaly/Desktop/Mentoring/Lesson 5/ConfigurationItemAttributeProject/settings/settings.txt";
        
        var testClass = new SomeClass(){Title = "Unchanged", Age = 23, Id = 0};

        testClass.SaveSettings();
        testClass.Title = "Changed";
        testClass.Id++;
        Console.WriteLine(testClass.Title);
        Console.WriteLine(testClass.Id);
        testClass.LoadSettings();
        Console.WriteLine(testClass.Title);
        Console.WriteLine(testClass.Id);


    }
}