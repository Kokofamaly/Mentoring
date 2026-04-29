using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Models;
using FileCabinetSoftware.Repository;
using FileCabinetSoftware.Services;

namespace FileCabinetSoftware;

public class Program
{
    public static void Main(string[] args)
    {
        Document locBook = new LocalizedBook
        {
            Title = "Clean Code",
            DatePublished = new DateOnly(2008, 8, 1),
            ISBN = "978-0132350884",
            Authors = new List<string>
            {
                "Robert C. Martin"
            },
            OriginalPublisher = "Prentice Hall",
            LocalPublisher = "ЭКСМО",
            CountryOfLocalization = "Russia",
            
        };
        var service = new DocumentService(new FileRepository());
        service.Save(locBook);
        var doc = service.GetDocument(Enums.DocumentType.LocalizedBook, 2);
        Console.WriteLine("Title: " + doc?.Title);
        
    }
}