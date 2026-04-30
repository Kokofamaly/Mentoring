using System.Diagnostics;
using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Caching;
using FileCabinetSoftware.Models;
using FileCabinetSoftware.Repository;
using FileCabinetSoftware.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileCabinetSoftware;

public class Program
{
    public static void Main(string[] args)
    {

        var services = new ServiceCollection();
        services.AddMemoryCache();
        services.AddSingleton<ICachePolicy, CachePolicy>();
        services.AddSingleton<IDocumentRepository, FileRepository>();
        services.AddSingleton<DocumentService>();

        var documentService = services.BuildServiceProvider().GetRequiredService<DocumentService>();

        var book = documentService.GetDocument(Enums.DocumentType.Book, 1);


    }
}