using Abstractions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ConfigurationManagerConfigurationProvider;

public class ConfigurationManagerConfigurationProvider : Abstractions.IConfigurationProvider
{
    private IConfigurationRoot _config;
    private readonly string _path;

    public ConfigurationManagerConfigurationProvider(string path){
        
        _path = path;
        _config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "settings"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
    }

    public void Save(string key, object value)
    {
        var json = File.ReadAllText(_path);
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;

        dict[key] = value;
        
        var updatedJson = JsonSerializer.Serialize(dict, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        File.WriteAllText(_path, updatedJson);

        _config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "settings"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
    public string? Load(string key)
    {
        return _config[key];
    }
}
