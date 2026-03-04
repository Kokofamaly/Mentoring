using System;
using Task2;

namespace Task1Console;

public class Program
{
    public static void Main(string[] args)
    {
        string username = args.Length > 0 ? args[0] : "User";
        var hello = new HelloLibrary();
        Console.WriteLine(hello.GetMessage(username));
    }
}