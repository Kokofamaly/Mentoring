using FileCabinetSoftware.Enums;

namespace FileCabinetSoftware.Abstractions.Interfaces;

public interface IDocumentRepository
{
    public void Save(Document item);
    public IEnumerable<Document> SearchById(int id);
    public IEnumerable<Document> SearchByType(DocumentType type);
    public Document? GetDocument(DocumentType type, int id);
}