using FileCabinetSoftware.Models;
using FileCabinetSoftware.Repository;
using FileCabinetSoftware.Enums;
using Xunit;

public class FileRepositoryTests : IDisposable
{
    private readonly string _testDir;
    private readonly FileRepository _repo;

    public FileRepositoryTests()
    {
        _testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _repo = new FileRepository(_testDir);
    }

    [Fact]
    public void Save_ShouldCreateFile()
    {
        var book = new Book
        {
            Title = "Test",
            ISBN = "123",
            DatePublished = DateOnly.FromDateTime(DateTime.Now)
        };

        _repo.Save(book);

        var files = Directory.GetFiles(_testDir);

        Assert.Single(files);
    }

    [Fact]
    public void GetDocument_ShouldReturnSavedDocument()
    {
        var book = new Book
        {
            Title = "Test Book",
            ISBN = "123",
            DatePublished = DateOnly.FromDateTime(DateTime.Now)
        };

        _repo.Save(book);

        var result = _repo.GetDocument(DocumentType.Book, 1);

        Assert.NotNull(result);
        Assert.Equal("Test Book", result.Title);
    }

    [Fact]
    public void SearchByType_ShouldReturnCorrectDocuments()
    {
        _repo.Save(new Book { Title = "Book1", DatePublished = DateOnly.FromDateTime(DateTime.Now) });
        _repo.Save(new Book { Title = "Book2", DatePublished = DateOnly.FromDateTime(DateTime.Now) });

        var results = _repo.SearchByType(DocumentType.Book).ToList();

        Assert.Equal(2, results.Count);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, true);
    }
}