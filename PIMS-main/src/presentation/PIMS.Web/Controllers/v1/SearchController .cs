using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using PIMS.Domain;
using PIMS.Web.Controllers.Base;
using Nest;

namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public SearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexDocument(PdfDocumentData document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (response.IsValid)
                return Ok();
            else
                return BadRequest(response.OriginalException.Message);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string author, string keywords, string publisher)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs");

            var response = await _elasticClient.SearchAsync<PdfDocumentData>(s => s
                .Query(q => q.Bool(b => b
                    .Must(
                        mu => author != null ? mu.Match(m => m.Field(f => f.Author).Query(author)) : null,
                        mu => keywords != null ? mu.Match(m => m.Field(f => f.Keywords).Query(keywords)) : null,
                        mu => publisher != null ? mu.Match(m => m.Field(f => f.Publisher).Query(publisher)) : null
                    ))));

            if (!response.IsValid)
                return BadRequest("Search failed");

            return Ok(response.Documents);
        }
        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine("Client", "public", "pdfs", fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound(filePath);

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf", fileName);
        }
    }
}

