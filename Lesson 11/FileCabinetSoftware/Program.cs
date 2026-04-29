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

        var service = new DocumentService(new FileRepository());
        Document mag = service.GetDocument(Enums.DocumentType.Magazine, 1);

        if(mag == null) throw new Exception();

        var props = mag.GetType().GetProperties();
        foreach(var prop in props)
        {
            Console.WriteLine(prop.Name + ": " + prop.GetValue(mag));
        }
    }
}