using Microsoft.Extensions.Configuration;
using RestClient;
using System.Net.Http.Json;

IConfiguration config = new  ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();

string url = config["Url"];

using var client = new HttpClient() { BaseAddress = new Uri(url) };

try
{
    var response = await client.GetAsync("api/products");

    if (response.IsSuccessStatusCode)
    {
        var prods = await response.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
        foreach(var p in prods)
        {
            Console.WriteLine(p.ProductName);
        }

        var prod = prods.FirstOrDefault();

        if (prod == null) return;

        prod.ProductName = prod.ProductName.Replace("- old", "- updated");

        response = await client.PutAsJsonAsync("api/products/" + prod.ProductId, prod);
        
        Console.WriteLine(response.StatusCode);

        var updatedProd = await (await client.GetAsync($"api/products/{prod.ProductId}")).Content.ReadFromJsonAsync<ProductModel>();

        Console.WriteLine(updatedProd?.ProductName);


    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}