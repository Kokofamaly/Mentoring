using FileCabinetSoftware.Abstractions.Interfaces;
using FileCabinetSoftware.Abstractions;
using FileCabinetSoftware.Enums;
using System.Text.Json;
using FileCabinetSoftware.Extensions;
using System.Text.Json.Serialization;
using System.Drawing;

namespace FileCabinetSoftware.Repository;

public class FileRepository : IDocumentRepository
{
    private readonly JsonSerializerOptions jsOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new JsonStringEnumConverter()}
        };
    private readonly string _path;
    public FileRepository() : this(Path.Combine(Directory.GetCurrentDirectory(), "Storage")){}
    public FileRepository(string path)
    {
        _path = String.IsNullOrWhiteSpace(path) ? Path.Combine(Directory.GetCurrentDirectory(), "Storage") : path;
        Directory.CreateDirectory(_path);
    }
    public void Save(Document item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        item.Id = GetNextId(item.Type);
        var fileName = $"{item.Type.ToString().ToLowerInvariant()}_#{item.Id}.json";
        var filePath = Path.Combine(_path, fileName);

        if(File.Exists(filePath)) 
            throw new InvalidOperationException($"The file already exists: \n{fileName}");

        var json = JsonSerializer.Serialize(item, item.GetType() ,jsOptions);
        File.WriteAllText(filePath, json);
    }
    public IEnumerable<Document> SearchById(int id)
    {

        var files = Directory.GetFiles(_path, "*.json");

        foreach(var file in files)
        {
            if(!TryParseFileName(file, out var docType, out int docId))
                continue;
            if(docId != id)
                continue;
            
            var jsonFile = File.ReadAllText(file);
            var doc = jsonFile.DeserializeJsonDocument();

            if(doc == null)
                continue;
            
            yield return doc;
            
        }
    }
    public IEnumerable<Document> SearchByType(DocumentType type)
    {
        var files = Directory.GetFiles(_path, "*.json");
        
        foreach(var file in files)
        {
            if(!TryParseFileName(file, out var docType, out int docId))
                continue;
            if(docType != type)
                continue;

            var jsonFile = File.ReadAllText(file);
            var doc = jsonFile.DeserializeJsonDocument();

            if(doc == null)
                continue;

            yield return doc;
        }
    }
    public Document? GetDocument(DocumentType type, int id)
    {

        var filePath = Path.Combine(_path, $"{type.ToString().ToLowerInvariant()}_#{id}.json");
        
        return File.Exists(filePath) 
        ? File.ReadAllText(filePath).DeserializeJsonDocument() 
        : null;
    }

    private bool TryParseFileName(string fileName, out DocumentType documentType, out int id)
    {
        documentType = default;
        id = default;

        var typeAndId = Path.GetFileNameWithoutExtension(fileName).Split("_#");

        if (typeAndId.Length != 2)
            return false;

        if (!int.TryParse(typeAndId[1], out id))
            return false;

        if (!Enum.TryParse<DocumentType>(typeAndId[0], true, out documentType))
            return false;

        return true;
    }

    private int GetNextId(DocumentType type)
    {
        var files = Directory.GetFiles(_path, $"{type.ToString().ToLowerInvariant()}_#*.json");

        int max = 0;

        foreach (var file in files)
        {
            var name = Path.GetFileNameWithoutExtension(file);
            var typeAndId = name.Split("_#");

            if (typeAndId.Length != 2)
                continue;

            if (int.TryParse(typeAndId[1], out var id))
                if (id > max)
                    max = id;
        }

        return max + 1;
    }
}