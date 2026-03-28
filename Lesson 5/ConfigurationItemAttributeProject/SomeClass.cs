using ConfigurationItemAttributeProject.Enums;

namespace ConfigurationItemAttributeProject;

public class SomeClass : ConfigurationComponentBase
{
    [ConfigurationItem("Title", ProviderType.File)]
    public string Title { get; set; }
    public int Age { get; set; }
    [ConfigurationItem("Id", ProviderType.Config)]
    public int Id { get; set; }
    

}