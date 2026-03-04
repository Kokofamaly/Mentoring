using System;

namespace Task1Console;

public class Program
{
    public static void Main(string[] args)
    {
        string username = args.Length > 0 ? args[0] : "User";
        Console.WriteLine($"Hello, {username}");
    }
}