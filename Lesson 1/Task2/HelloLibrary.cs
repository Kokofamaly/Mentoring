using System;

namespace Task2;

public class HelloLibrary
{
    public string GetMessage(string username)
    {
        if(String.IsNullOrEmpty(username)) username = "User";
        return $"{DateTime.UtcNow}:\tHello, {username}";
    }
}
