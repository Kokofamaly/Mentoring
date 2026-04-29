using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class LocalizedBook : Document
{
    public string ISBN { get; set; }
    public List<string> Authors { get; set; }
    public int NumberOfPages { get; set; }
    public string OriginalPublisher { get; set; }
    public string CountryOfLocalization { get; set; }
    public string LocalPublisher { get; set; }
    public override DocumentType Type => DocumentType.LocalizedBook;

}