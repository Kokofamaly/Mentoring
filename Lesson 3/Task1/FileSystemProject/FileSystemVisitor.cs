using System.IO;

namespace FileSystemProject.FileSystemVisitor;

public class FileSystemVisitor
{
    private Predicate<string> _predicate;

    // private string? _path;
    private DirectoryInfo _directoryInfo;
    public FileSystemInfo fileSystemInfo;
    public FileSystemVisitor(string predefinedPath = null!)
    {
        if(predefinedPath == null) _directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        else _directoryInfo = new DirectoryInfo(predefinedPath);
    }
    public FileSystemVisitor(Predicate<string> predicate, string predefinedPath = null!)
    {
        _predicate = predicate;

        if(predefinedPath == null) _directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        else _directoryInfo = new DirectoryInfo(predefinedPath);
    }

    public IEnumerable<string> GetFiles()
    {
        var directories = _directoryInfo.GetDirectories();
        foreach(var d in directories)
        {
            yield return d.Name;
        }
    }

    public IEnumerable<FileSystemInfo> Traverse(string path)
    {
        foreach(var fs in _directoryInfo.GetFileSystemInfos(path))
        {
            yield return fs;
            if(fs is DirectoryInfo subDir)
            {
                foreach(var item in this.Traverse(fs.FullName){
                    yield return item
                }
            }
        }
    }

}