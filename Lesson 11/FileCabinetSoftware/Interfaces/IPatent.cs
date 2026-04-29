namespace FileCabinetSoftware.Interfaces;

interface IPatent : IDocument
{
    public string UniqueId { get; set; }
    public List<string> Authors { get; set; }
    public DateOnly ExpirationDate { get; set; }
    
}