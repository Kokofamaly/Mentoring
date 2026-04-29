namespace FileCabinetSoftware.Interfaces;

interface IDocument
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly DatePublished{ get; set; }
}