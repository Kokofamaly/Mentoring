using System;

namespace Task2;

public class HelloClass
{
    public string GetMessage(string username)
    {
        return $"{DateTime.UtcNow}:Hello, {username}";
    }
}
