using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class Patent : Document
{
    public string UniqueId { get; set; }
    public List<string> Authors { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public override DocumentType Type => DocumentType.Patent;
    
}