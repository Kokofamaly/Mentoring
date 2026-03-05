using System;

namespace Task2;

public class HelloLibrary
{
    public string GetMessage(string username = "User")
    {
        return $"{DateTime.UtcNow}:Hello, {username}";
    }
}
