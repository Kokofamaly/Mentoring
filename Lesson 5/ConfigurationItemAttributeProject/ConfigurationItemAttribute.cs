using ConfigurationItemAttributeProject.Enums;

namespace ConfigurationItemAttributeProject;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationItemAttribute : Attribute
{
    public string? SettingName { get; set; }
    public ProviderType Provider { get; set; }
    public ConfigurationItemAttribute(string settingName, ProviderType providerType)
    {
        SettingName = settingName;
        Provider = providerType;
    }

}