using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class Magazine : Document
{
    public string Publisher { get; set; } = string.Empty;
    public int ReleaseNumber { get; set; }
    public override DocumentType Type => DocumentType.Magazine;
}