using ConfigurationItemAttributeProject.Enums;

namespace ConfigurationItemAttributeProject;

public class SomeClass : ConfigurationComponentBase
{
    [ConfigurationItem("Name", ProviderType.File)]

    public string? Name { get; set; }
    
    [ConfigurationItem("Age", ProviderType.Config)]
    public int Age { get; set; }
    [ConfigurationItem("Weight", ProviderType.Config)]
    public float Weight { get; set; }
    [ConfigurationItem("WaitingTime", ProviderType.File)]
    public TimeSpan WaitingTime { get; set; }
    

}

