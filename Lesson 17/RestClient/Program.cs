using Microsoft.Extensions.Configuration;
using RestClient;
using System.Net.Http.Json;

IConfiguration config = new  ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();

string url = config["Url"];

using var client = new HttpClient() { BaseAddress = new Uri(url) };

try
{
    var response = await client.GetAsync("api/products");

    response.EnsureSuccessStatusCode();

    var prods = await response.Content.ReadFromJsonAsync<IEnumerable<ProductModel>>();
    
    if(prods == null || !prods.Any()) return;

    foreach(var p in prods)
    {
        Console.WriteLine(p.ProductName);
    }

    var prodToUpdate = prods.First();

    prodToUpdate.ProductName = "Updated Name";

    var updateResponse = await client.PutAsJsonAsync("api/products/" + prodToUpdate.ProductId, prodToUpdate);
    
    updateResponse.EnsureSuccessStatusCode();

    var updatedProd = await client.GetFromJsonAsync<ProductModel>($"api/products/{prodToUpdate.ProductId}");

    Console.WriteLine(updatedProd?.ProductName);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}