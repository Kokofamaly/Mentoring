using System.Net;

string url = "http://localhost:8888/MyNameByCookies/";

var cookies = new CookieContainer();
var handler = new HttpClientHandler()
{
    CookieContainer = cookies,
    AllowAutoRedirect = false
};

using var client = new HttpClient(handler);

try
{
    var response = await client.GetAsync(url);
    var cookie = cookies.GetCookies(new Uri(url))["MyName"];

    if (cookie != null)
    {
        Console.WriteLine(cookie.Value);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}