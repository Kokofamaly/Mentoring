using System.Net;
using System.Text;

const string Url = "http://localhost:8888/";
string name = "Vadim";

HttpListener listener = new();

listener.Prefixes.Add(Url);

listener.Start();

Console.WriteLine($"Listening on {Url}");

while (true)
{
    HttpListenerContext context = await listener.GetContextAsync();

    string resource =
        context.Request.Url?.AbsolutePath.Trim('/') ?? string.Empty;

    Console.WriteLine($"Request: {resource}");

    switch (resource.ToLower())
    {
        case "myname":
            await GetMyName(context, name);
            break;

        case "exit":
            listener.Stop();
            listener.Close();
            return;

        default:
            context.Response.StatusCode = 404;
            context.Response.Close();
            break;
    }
}

static async Task GetMyName(HttpListenerContext context, string name)
{

    byte[] buffer = Encoding.UTF8.GetBytes(name);

    context.Response.ContentType = "text/plain";
    context.Response.ContentLength64 = buffer.Length;
    await context.Response.OutputStream.WriteAsync(buffer);

    context.Response.Close();
}