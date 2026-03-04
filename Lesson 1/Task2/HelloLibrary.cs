using System;

namespace Task2;

public class HelloLibrary
{
    public string GetMessage(string username)
    {
        return $"{DateTime.UtcNow}:Hello, {username}";
    }
}
