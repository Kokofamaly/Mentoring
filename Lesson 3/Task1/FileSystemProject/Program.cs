namespace FileSystemProject;

public class Program
{
    public static void Main(string[] args)
    {
        var fileVisitor = new FileSystemVisitor();
        foreach(var fs in fileVisitor.Traverse())
        {
            Console.WriteLine(fs.Name);
        }

    }
}