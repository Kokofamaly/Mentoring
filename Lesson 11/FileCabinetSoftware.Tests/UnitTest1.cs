using FileCabinetSoftware.Repository;
using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;
using FileCabinetSoftware.Models;
using Xunit;

public class FileRepositoryTests
{
    private string CreateTempFolder()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(path);
        return path;
    }

    [Fact]
    public void Save_Then_GetDocument_ShouldReturnSameDocument()
    {
        // arrange
        var path = CreateTempFolder();
        var repo = new FileRepository(path);

        Document doc = new Book
        {
            Title = "Test",
        };

        // act
        repo.Save(doc);
        var result = repo.GetDocument(DocumentType.Book, doc.Id);

        // assert
        Assert.NotNull(result);
        Assert.Equal(doc.Id, result!.Id);
        Assert.Equal(doc.Type, result.Type);
    }
}