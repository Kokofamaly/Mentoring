using Microsoft.VisualBasic;

string url = "http://localhost:8888/";
string[] requestStatuses = {"Success", "Redirection", "ClientError", "ServerError", "Information"};
var handler = new HttpClientHandler()
{
    AllowAutoRedirect = false
};

using var client = new HttpClient(handler);

try
{
    var response = await client.GetAsync($"{url}MyNameByHeader/");
    if (response.Headers.TryGetValues("X-MyName", out var values))
    {
        Console.WriteLine(values.First());
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}