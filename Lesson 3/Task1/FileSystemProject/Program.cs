namespace FileSystemProject;

public class Program
{
    public static void Main(string[] args)
    {
        var fileVisitor = new FileSystemVisitor();

        fileVisitor.Started += (s, e) => Console.WriteLine("Traversing Started...");
        fileVisitor.Finished += (s, e) => Console.WriteLine("Traversing Completed...");

        fileVisitor.FileFound += (s, e) => Console.WriteLine($"File Found: {e.Item?.Name}");
        fileVisitor.DirectoryFound += (s, e) => Console.WriteLine($"Directory Found: {e.Item?.Name}");
        fileVisitor.FilteredFileFound += (s, e) => e.Exclude = true;
        fileVisitor.FilteredDirectoryFound += (s, e) => Console.WriteLine($"Filtered Directory Found: {e.Item?.Name}");
        
        int count = fileVisitor.Traverse().Count();
        Console.WriteLine(count);

        foreach(var fs in fileVisitor.Traverse())
        {
            Console.WriteLine(fs.Name);
        }




    }
}