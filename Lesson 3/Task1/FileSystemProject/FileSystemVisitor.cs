using System.IO;
using FileSystemProject.Task2;

public class FileSystemVisitor
{
    private bool _started = false;
    private bool _finished = true;
    private bool _abort;
    public event EventHandler? Started;
    public event EventHandler? Finished;
    public event EventHandler<FileSystemVisitorEventArgs>? DirectoryFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredDirectoryFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredFileFound;

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
        if (!_started)
        {
            _started = true;
            _finished = false;
            Started?.Invoke(this, EventArgs.Empty);
        }

        if(currentDirectory == null) 
            currentDirectory = _directoryInfo;
        
        foreach(var fs in currentDirectory.GetFileSystemInfos())
        {
            FileSystemVisitorEventArgs args = new FileSystemVisitorEventArgs(fs);

            if(fs is DirectoryInfo)
                DirectoryFound?.Invoke(this, args);
            else
                FileFound?.Invoke(this, args);

            if(_filter(fs))
            {
                if(fs is DirectoryInfo)
                    FilteredDirectoryFound?.Invoke(this, args);
                else
                    FilteredFileFound?.Invoke(this, args);

                if(!args.Exclude)
                    yield return fs;

                if(args.Abort)
                    _abort = true;
                    
                if(_abort)
                {
                    _started = false;
                    _finished = true;
                    Finished?.Invoke(this, EventArgs.Empty);
                    yield break;
                }
            }
            
            if(fs is DirectoryInfo directory) 
                foreach(var item in Traverse(directory))
                {
                    if(_abort) yield break;
                    
                    yield return item;
                }

            
        }

        if (!_finished)
        {
            _started = false;
            _finished = true;
            Finished?.Invoke(this, EventArgs.Empty);
        }

    }
}