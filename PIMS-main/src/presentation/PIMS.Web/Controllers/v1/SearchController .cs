using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using PIMS.Domain;
using PIMS.Web.Controllers.Base;
using Nest;
using System.Net;

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
                var pdfFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs"), "*.pdf");
                var searchResults = new List<string>();

                foreach (var pdfFile in pdfFiles)
                {
                    var fileName = Path.GetFileName(pdfFile);
                    var pdfContent = System.IO.File.ReadAllText(pdfFile);
                    if (PdfContainsQuery(pdfContent, query))
                    {
                        searchResults.Add(fileName);
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
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs", fileName);
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

        private bool PdfContainsQuery(string content, string query)
        {
            content = content.ToLower();
            query = query.ToLower();
            return content.Contains(query);
        }
        // Метод для поиска PDF файлов по автору.
        [HttpGet("search-by-author")]
        public IActionResult SearchByAuthor(string author)
        {
            try
            {
                var pdfFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Client", "public", "pdfs"), "*.pdf");
                var searchResults = new List<string>();

                foreach (var pdfFile in pdfFiles)
                {
                    var fileName = Path.GetFileName(pdfFile);
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

        // Пример использования метода поиска по автору и обработки результатов.
        public IActionResult ExampleSearchAndProcess()
        {
            var author = "Пушкин";
            var searchResponse = SearchByAuthor(author);

            if (searchResponse is ObjectResult objectResult)
            {
                var pdfFiles = (List<string>)objectResult.Value;
                // Теперь у вас есть список PDF файлов, связанных с автором "Пушкин".
                // Можете использовать этот список в соответствии с вашими потребностями.
                return Ok(pdfFiles);
            }
            else
            {
                // Обрабатываем ошибку, если запрос завершился неудачей.
                var errorMessage = "Произошла ошибка при выполнении запроса.";
                // Выводим сообщение об ошибке или предпринимаем другие действия.
                return StatusCode(500, errorMessage);
            }
        }
    }
}
