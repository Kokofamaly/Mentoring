using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class Book : Document
{
    public string ISBN { get; set; } = string.Empty;  
    public List<string> Authors { get; set; } = new();
    public int NumberOfPages { get; set; } = 0;
    public string Publisher { get; set; } = string.Empty;
    public override DocumentType Type => DocumentType.Book;
}