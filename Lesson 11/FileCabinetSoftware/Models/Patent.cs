using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class Patent : Document
{
    public string UniqueId { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = new();
    public DateOnly ExpirationDate { get; set; }
    public override DocumentType Type => DocumentType.Patent;
    
}