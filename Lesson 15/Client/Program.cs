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
    foreach(var status in requestStatuses)
    {
        var response = await client.GetAsync($"{url}{status}/");
        Console.WriteLine((int)response.StatusCode);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}