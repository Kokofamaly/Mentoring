string url = "http://localhost:8888/MyName/";

var client = new HttpClient();

try
{
    string response = await client.GetStringAsync(url);
    Console.WriteLine(response);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}