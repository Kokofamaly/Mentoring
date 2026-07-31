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
        Console.WriteLine("Product name \t---->\t" + p.ProductName + "\n");
    }

    var prodToUpdate = prods.Select(p => new ProductUpdateModel
    {
        ProductId = p.ProductId,
        ProductName = p.ProductName,
        SupplierId = null,
        CategoryId = null,
        QuantityPerUnit = p.QuantityPerUnit,
        UnitPrice = p.UnitPrice,
        UnitsInStock = p.UnitsInStock,
        UnitsOnOrder = p.UnitsOnOrder,
        ReorderLevel = p.ReorderLevel,
        Discontinued = p.Discontinued
    }).FirstOrDefault();

    if (prodToUpdate == null) return;

        

    prodToUpdate.ProductName = Guid.NewGuid().ToString();

    response = await client.PutAsJsonAsync("api/products/" + prodToUpdate.ProductId, prodToUpdate);
        
    var updatedProd = await (await client.GetAsync($"api/products/{prodToUpdate.ProductId}")).Content.ReadFromJsonAsync<ProductModel>();

    Console.WriteLine("Updated Product name \t---->\t" + updatedProd?.ProductName + "\n\n");



}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}