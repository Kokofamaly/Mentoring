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

        case "information":
            await Information(context);
            break;

        case "success":
            await Success(context);
            break;

        case "redirection":
            await Redirection(context);
            break;

        case "clienterror":
            await ClientError(context);
            break;

        case "servererror":
            await ServerError(context);
            break;

        case "mynamebyheader":
            await GetMyNameByHeader(context, name);
            break;        

        case "mynamebycookies":
            await GetMyNameByCookies(context, name);
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

static Task SendStatus(HttpListenerContext context, HttpStatusCode statusCode)
{
    context.Response.StatusCode = (int)statusCode;
    context.Response.Close();
    return Task.CompletedTask;
}

static Task Information(HttpListenerContext context) => SendStatus(context, HttpStatusCode.Processing);
static Task Success(HttpListenerContext context) => SendStatus(context, HttpStatusCode.OK);
static Task Redirection(HttpListenerContext context) => SendStatus(context, HttpStatusCode.MovedPermanently);
static Task ClientError(HttpListenerContext context) => SendStatus(context, HttpStatusCode.BadRequest);
static Task ServerError(HttpListenerContext context) => SendStatus(context, HttpStatusCode.BadGateway);

static Task GetMyNameByHeader(HttpListenerContext context, string name)
{
    context.Response.AddHeader("X-MyName", name);
    context.Response.Close();
    return Task.CompletedTask;
}
static Task GetMyNameByCookies(HttpListenerContext context, string name)
{
    var cookie = new Cookie("MyName", name);
    context.Response.SetCookie(cookie);
    context.Response.Close();
    return Task.CompletedTask;
}