using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class LocalizedBook : Document
{
    public string ISBN { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = new();
    public int NumberOfPages { get; set; } = 0;
    public string OriginalPublisher { get; set; } = string.Empty;
    public string CountryOfLocalization { get; set; } = string.Empty;
    public string LocalPublisher { get; set; } = string.Empty;
    public override DocumentType Type => DocumentType.LocalizedBook;

}