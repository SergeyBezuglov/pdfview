using Microsoft.AspNetCore.Mvc;
using Nest;
using PIMS.Domain;

public class DocumentsController : ControllerBase
{
    private readonly PdfMetadataExtractor _metadataExtractor;

    public DocumentsController(IElasticClient elasticClient)
    {
        _metadataExtractor = new PdfMetadataExtractor(elasticClient);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        // Логика обработки файла и извлечения данных
        var documentData = new PdfDocumentData(); // Заполните данными
        await _metadataExtractor.IndexPdfDocumentAsync(documentData);
        return Ok();
    }
}