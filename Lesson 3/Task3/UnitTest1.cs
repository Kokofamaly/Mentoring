

namespace Task3;

public class UnitTest1
{
    [Fact]
    public void Finished_ShoulBeRaisedOnce()
    {

        var fileVisitor = new FileSystemVisitor();
        int finishedCount = 0;

        fileVisitor.Finished += (s, e) =>
        {
            finishedCount++;
            Console.WriteLine($"Finisher: {0}", finishedCount);
        };
        fileVisitor.Traverse().ToList();

        Assert.Equal(1, finishedCount);
    
    }

    [Fact]
    public void Started_ShouldBeRaisedOnce()
    {
        var fileVisitor = new FileSystemVisitor();
        int startedCount = 0;

        fileVisitor.Started += (s, e) => startedCount++;

        fileVisitor.Traverse().ToList();

        Assert.Equal(1, startedCount);
    }

    [Fact]
    public void FileFound_ShoulBeRaised()
    {
        var fileVisitor = new FileSystemVisitor();
        int count = 0;

        fileVisitor.FileFound += (s, e) => count++;

        fileVisitor.Traverse().ToList();

        Assert.True(count > 0);
    }

    [Fact]
    public void Exclude_ExcludesFiles()
    {
        var fileVisitor = new FileSystemVisitor();

        fileVisitor.FilteredFileFound += (s, e) => e.Exclude = true;
        fileVisitor.FilteredDirectoryFound += (s, e) => e.Exclude = true;


        var result = fileVisitor.Traverse().ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void Abort_ShouldStopTraverse()
    {
        var fileVisitor = new FileSystemVisitor();
        int count = 0;

        fileVisitor.FileFound += (s,e) =>
        {
            count++;
            if (count == 1) e.Abort = true;
        };
        fileVisitor.Traverse().ToList();

        Assert.True(count <= 1);
    }

    [Fact]
    public void Traverse_RetrunsFileSystemInfos()
    {
        var fileVisitor = new FileSystemVisitor();
        
        var fs = fileVisitor.Traverse().First();

        Assert.True(fs is FileSystemInfo);
    }

    [Fact]
    public void Traverse_FiltersFiles()
    {
        var fileVisitor = new FileSystemVisitor(fs => fs.Extension == ".dll");

        var result = fileVisitor.Traverse().ToList();

        Assert.All(result, x => Assert.Equal(x.Extension, ".dll"));
    }
}
