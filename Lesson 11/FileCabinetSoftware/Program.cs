using System.Diagnostics;
using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Caching;
using FileCabinetSoftware.Models;
using FileCabinetSoftware.Repository;
using FileCabinetSoftware.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace FileCabinetSoftware;

public class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false).Build();

        
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        services.Configure<CacheSettings>(options =>
        {
            config.GetSection("CachePolicies").Bind(options);
        });

        services.AddMemoryCache();
        services.AddSingleton<ICachePolicy, CachePolicy>();
        services.AddSingleton<IDocumentRepository, FileRepository>();
        services.AddSingleton<DocumentService>();
        
        var documentService = services.BuildServiceProvider().GetRequiredService<DocumentService>();

        var book = documentService.GetDocument(Enums.DocumentType.Book, 1);
        Console.WriteLine(book.Title);


    }
}