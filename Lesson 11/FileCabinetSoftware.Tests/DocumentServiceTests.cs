using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Caching;
using FileCabinetSoftware.Enums;
using FileCabinetSoftware.Models;
using FileCabinetSoftware.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;
using Microsoft.Extensions.Options;

public class DocumentServiceTests
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions());
    // private readonly CachePolicy _policy = new();
    private CachePolicy CreatePolicy()
    {
        var settings = new CacheSettings
        {
            CachePolicies = new()
            {
                ["Book"] = new CachePolicyOptions
                {
                    Mode = CacheMode.Absolute,
                    Expiration = TimeSpan.FromMinutes(10)
                },
                ["LocalizedBook"] = new CachePolicyOptions
                {
                    Mode = CacheMode.Absolute,
                    Expiration = TimeSpan.FromMinutes(10)
                },
                ["Magazine"] = new CachePolicyOptions
                {
                    Mode = CacheMode.Disabled
                },
                ["Patent"] = new CachePolicyOptions
                {
                    Mode = CacheMode.Infinite
                }
            }
        };

        return new CachePolicy(Options.Create(settings));
    }
    

    [Fact]
    public void GetDocument_ShouldCacheResult()
    {
        var mockRepo = new Mock<IDocumentRepository>();

        var book = new Book
        {
            Id = 1,
            Title = "Cached Book",
            DatePublished = DateOnly.FromDateTime(DateTime.Now)
        };

        mockRepo.Setup(r => r.GetDocument(DocumentType.Book, 1))
                .Returns(book);

        var service = new DocumentService(mockRepo.Object, _cache, CreatePolicy());

        var firstCall = service.GetDocument(DocumentType.Book, 1);
        var secondCall = service.GetDocument(DocumentType.Book, 1);

        mockRepo.Verify(r => r.GetDocument(DocumentType.Book, 1), Times.Once);

        Assert.Equal(firstCall, secondCall);
    }

    [Fact]
    public void GetDocument_ShouldNotCache_WhenExpirationZero()
    {
        var mockRepo = new Mock<IDocumentRepository>();

        var magazine = new Magazine
        {
            Id = 1,
            Title = "Mag",
            DatePublished = DateOnly.FromDateTime(DateTime.Now)
        };

        mockRepo.Setup(r => r.GetDocument(DocumentType.Magazine, 1))
                .Returns(magazine);

        var service = new DocumentService(mockRepo.Object, _cache, CreatePolicy());

        service.GetDocument(DocumentType.Magazine, 1);
        service.GetDocument(DocumentType.Magazine, 1);

        mockRepo.Verify(r => r.GetDocument(DocumentType.Magazine, 1), Times.Exactly(2));
    }

    [Fact]
    public void GetDocument_ShouldReturnNull_WhenNotFound()
    {
        var mockRepo = new Mock<IDocumentRepository>();

        mockRepo.Setup(r => r.GetDocument(It.IsAny<DocumentType>(), It.IsAny<int>()))
                .Returns((Book?)null);

        var service = new DocumentService(mockRepo.Object, _cache, CreatePolicy());

        var result = service.GetDocument(DocumentType.Book, 999);

        Assert.Null(result);
    }
}