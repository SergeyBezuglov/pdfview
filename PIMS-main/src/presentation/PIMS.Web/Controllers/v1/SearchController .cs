using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        [HttpGet("search-pdf")]
        public IActionResult SearchPdf(string query)
        {
            try
            {
                var pdfFiles = Directory.GetFiles(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs"), "*.pdf");
                var searchResults = new List<string>();
                foreach (var pdfFile in pdfFiles)
                {
                    if (PdfContainsQuery(pdfFile, query))
                    {
                        searchResults.Add(System.IO.Path.GetFileName(pdfFile));
                    }
                }
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("download-pdf")]
        public IActionResult DownloadPdf(string fileName)
        {
            try
            {
                var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs", fileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                return File(memory, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        private bool PdfContainsQuery(string filePath, string query)
        {
            using (PdfReader reader = new PdfReader(filePath))
            {
                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString().ToLowerInvariant().Contains(query.ToLowerInvariant());
            }
        }


        [HttpGet("search-by-author")]
        public IActionResult SearchByAuthor(string author)
        {
            try
            {
                var pdfFiles = Directory.GetFiles(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs"), "*.pdf");
                var searchResults = new List<string>();
                foreach (var pdfFile in pdfFiles)
                {
                    var fileName = System.IO.Path.GetFileName(pdfFile);
                    var authors = GetAuthorsFromPdf(pdfFile);
                    if (authors.Contains(author, StringComparer.OrdinalIgnoreCase))
                    {
                        searchResults.Add(fileName);
                    }
                }
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Внутренняя ошибка сервера: {ex}");
            }
        }
        // Вспомогательный метод для извлечения информации об авторах из PDF.
        private IEnumerable<string> GetAuthorsFromPdf(string filePath)
        {
            // Возвращаем фиктивные авторы для демонстрации.
            return new List<string> { "Пушкин", "А.С. Пушкин" };
        }
    }
}