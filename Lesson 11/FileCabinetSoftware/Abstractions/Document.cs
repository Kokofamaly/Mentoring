using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Abstractions;

public abstract class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly DatePublished { get; set; }
    public abstract DocumentType Type { get; }
}