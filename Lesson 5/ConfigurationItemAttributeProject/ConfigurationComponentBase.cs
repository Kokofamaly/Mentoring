using System.Reflection;
using ConfigurationItemAttributeProject.Enums;
using ConfigurationItemAttributeProject.Interfaces;
using ConfigurationItemAttributeProject.ConfigurationProviders;

namespace ConfigurationItemAttributeProject;

public class ConfigurationComponentBase
{
    Dictionary<ProviderType, IConfigurationProvider> providers = new Dictionary<ProviderType, IConfigurationProvider>()
    {
        {ProviderType.File, new FileConfigurationProvider(Path.Combine(Directory.GetCurrentDirectory(), "settings", "settings.txt"))},
        {ProviderType.Config, new ConfigurationManagerConfigurationProvider(Path.Combine(Directory.GetCurrentDirectory(), "settings", "appsettings.json"))}
    };
    public void SaveSettings()
    {
        var props = this.GetType().GetProperties();
        foreach(var prop in props)
        {
            var attr = prop.GetCustomAttribute<ConfigurationItemAttribute>();
            if(attr == null) continue;
            var provider = providers[attr.Provider];
            var value = prop.GetValue(this);
            if(value == null) continue;
            provider.Save(attr.SettingName!, value);
        }
    }

    public void LoadSettings()
    {
        var props = this.GetType().GetProperties();
        foreach(var prop in props)
        {
            var attr = prop.GetCustomAttribute<ConfigurationItemAttribute>();
            if(attr == null) continue;
            var provider = providers[attr.Provider];
            var value = provider.Load(attr.SettingName!);
            if(value == null) continue;
            if(prop.PropertyType == typeof(TimeSpan))
            {
                if(TimeSpan.TryParse(value, out TimeSpan result))
                {
                    prop.SetValue(this, result);
                    continue;
                }
                continue;
            }
            var converted = Convert.ChangeType(value, prop.PropertyType);
            prop.SetValue(this, converted);
        }
    }
}