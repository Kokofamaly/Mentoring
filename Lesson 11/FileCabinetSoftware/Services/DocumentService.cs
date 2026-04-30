using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace FileCabinetSoftware.Services;

public class DocumentService
{
    private readonly IMemoryCache _cache;
    private readonly IDocumentRepository _repo;
    private readonly ICachePolicy _cachePolicy;

    public DocumentService(IDocumentRepository repo, IMemoryCache cache, ICachePolicy cachePolicy)
    {
        _repo = repo;
        _cache = cache;
        _cachePolicy = cachePolicy;
    }

    public void Save(Document document) => _repo.Save(document);
    public IEnumerable<Document> SearchById(int id) => _repo.SearchById(id);
    public IEnumerable<Document> SearchByType(DocumentType docType) => _repo.SearchByType(docType);
    public Document? GetDocument(DocumentType docType, int id)
    {
        var key = $"{docType}_#{id}";
        

        if(_cache.TryGetValue(key, out Document cachedDocument))
        {
            return cachedDocument;
        }
        
        var doc = _repo.GetDocument(docType, id);

        if(doc == null)
            return null;
        
        var cacheExpiration = _cachePolicy.GetCacheTimeExpiration(docType);

        if(cacheExpiration == TimeSpan.Zero)
            return doc;
        
        var options = new MemoryCacheEntryOptions();

        if(cacheExpiration == null)
            options.AbsoluteExpiration = DateTimeOffset.MaxValue;
        else
            options.AbsoluteExpirationRelativeToNow = cacheExpiration;

        _cache.Set<Document>(key, doc, options);

        return doc;
    }
}