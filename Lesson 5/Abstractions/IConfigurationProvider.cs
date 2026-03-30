namespace Abstractions;

public interface IConfigurationProvider
{
    public void Save(string key, object value);
    public string? Load(string key);
}
