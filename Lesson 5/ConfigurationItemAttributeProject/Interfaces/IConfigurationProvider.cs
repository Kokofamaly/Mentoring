namespace ConfigurationItemAttributeProject.Interfaces;

interface IConfigurationProvider
{
    public void Save(string key, object value);
    public string? Load(string key);
}