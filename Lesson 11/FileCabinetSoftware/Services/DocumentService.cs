using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Services;

public class DocumentService
{
    private readonly IDocumentRepository _repo;

    public DocumentService(IDocumentRepository repo)
    {
        _repo = repo;
    }

    public void Save(Document document) => _repo.Save(document);
    public IEnumerable<Document> SearchById(int id) => _repo.SearchById(id);
    public IEnumerable<Document> SearchByType(DocumentType docType) => _repo.SearchByType(docType);
    public Document? GetDocument(DocumentType docType, int id) => _repo.GetDocument(docType, id);
}