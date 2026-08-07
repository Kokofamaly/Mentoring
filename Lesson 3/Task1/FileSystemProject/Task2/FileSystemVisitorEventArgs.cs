using System;

namespace FileSystemProject.Task2;

public class FileSystemVisitorEventArgs : EventArgs
{
    public FileSystemInfo? Item { get; set; }
    public bool Abort { get; set; }
    public bool Exclude { get; set; }
    public FileSystemVisitorEventArgs(FileSystemInfo item) => Item = item;
}
