namespace FileCabinetSoftware.Interfaces;

interface IBook : IDocument
{
    public string ISBN { get; set; }    
    public List<string> Authors { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }
}