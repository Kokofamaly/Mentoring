using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Models;

public class Book : Document
{
    public string ISBN { get; set; }    
    public List<string> Authors { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }
    public override DocumentType Type => DocumentType.Book;
}