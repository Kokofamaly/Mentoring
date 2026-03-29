using System.Text;
using ConfigurationItemAttributeProject.Interfaces;

namespace ConfigurationItemAttributeProject.ConfigurationProviders;

public class FileConfigurationProvider : IConfigurationProvider
{
    public readonly string path;
    public FileConfigurationProvider(string path)
    {
        this.path = path;
    }
    public void Save(string key, object value)
    {
        var lines = File.ReadAllLines(path).ToList();
        for(int i = 0; i < lines.Count; i++)
        {
            var nameAndValue = lines[i].Split('=', 2);
            if(nameAndValue[0] == key)
            {
                lines[i] = $"{key}={value}";
                break;
            }   
        }
        if (!lines.Contains($"{key}={value}"))
        {
            lines.Add($"{key}={value}");
        }

        File.WriteAllLines(path, lines);
    }
    public string? Load(string key)
    {
        var lines = File.ReadAllLines(path).ToList();
        foreach(var line in lines)
        {
            var nameAndValue = line.Split('=', 2);
            if(key == nameAndValue[0])
            {
                return nameAndValue[1];
            }
        }
        return null;
    }
}
