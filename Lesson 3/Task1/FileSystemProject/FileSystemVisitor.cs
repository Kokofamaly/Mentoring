using System.IO;

public class FileSystemVisitor
{
    private Predicate<FileSystemInfo> _filter;
    private DirectoryInfo _directoryInfo;
    public FileSystemVisitor()
    {
        _directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        _filter = fs => true;
    }

    public FileSystemVisitor(string path)
    {
        _directoryInfo = new DirectoryInfo(path);
        _filter = fs => true;
    }

    public FileSystemVisitor(string path, Predicate<FileSystemInfo> filter)
    {
        _directoryInfo = new DirectoryInfo(path);
        _filter = filter;
    }

    public IEnumerable<FileSystemInfo> Traverse(DirectoryInfo currentDirectory = null!)
    {
        if(currentDirectory == null) 
            currentDirectory = _directoryInfo;
        
        foreach(var fs in currentDirectory.GetFileSystemInfos())
        {
            if(_filter(fs)) 
                yield return fs;
            
            if(fs is DirectoryInfo directory) 
                foreach(var item in Traverse(directory))
                {
                    yield return item;
                }

            
        }
    }
}