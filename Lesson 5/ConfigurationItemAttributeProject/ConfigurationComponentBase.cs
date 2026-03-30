using System.Reflection;
using ConfigurationItemAttributeProject.Enums;
using Abstractions;

namespace ConfigurationItemAttributeProject;

public class ConfigurationComponentBase
{
    IConfigurationProvider? provider;

    Dictionary<ProviderType, string> providers = new Dictionary<ProviderType, string>()
    {
        {ProviderType.File, "ConfigurationProviders/FileConfigurationProvider.dll"},
        {ProviderType.Config, "ConfigurationProviders/ConfigurationManagerConfigurationProvider.dll"}
    };
    public void SaveSettings()
    {
        var props = this.GetType().GetProperties();
        foreach(var prop in props)
        {
            var attr = prop.GetCustomAttribute<ConfigurationItemAttribute>();

            if(attr == null) continue;

            var path = providers[attr.Provider];
            var assembly = Assembly.LoadFrom(path);

            var type = assembly.GetTypes().First(t => typeof(IConfigurationProvider).IsAssignableFrom(t) && !t.IsInterface);

            switch (attr.Provider)
            {
                case ProviderType.File:
                    provider = (IConfigurationProvider)Activator.CreateInstance(type, "settings/settings.txt")!;
                    break;
                case ProviderType.Config:
                    provider = (IConfigurationProvider)Activator.CreateInstance(type, "settings/appsettings.json")!;
                    break;
            }

            var value = prop.GetValue(this);

            if(value == null) continue;

            provider!.Save(attr.SettingName!, value);
        }
    }

    public void LoadSettings()
    {
        var props = this.GetType().GetProperties();
        foreach(var prop in props)
        {
            var attr = prop.GetCustomAttribute<ConfigurationItemAttribute>();

            if(attr == null) continue;

            var path = providers[attr.Provider];
            var assembly = Assembly.LoadFrom(path);

            var type = assembly.GetTypes().First(t => typeof(IConfigurationProvider).IsAssignableFrom(t) && !t.IsInterface);
            

            switch (attr.Provider)
            {
                case ProviderType.File:
                    provider = (IConfigurationProvider)Activator.CreateInstance(type, "settings/settings.txt")!;
                    break;
                case ProviderType.Config:
                    provider = (IConfigurationProvider)Activator.CreateInstance(type, "settings/appsettings.json")!;
                    break;
            }

            var value = provider!.Load(attr.SettingName!);

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