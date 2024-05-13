using Microsoft.AspNetCore.Mvc;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PIMS.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using PIMS.Application;
using Path = System.IO.Path;
namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfDocumentRepository _pdfDocumentRepository;
        private readonly IWebHostEnvironment  _hostingEnvironment;

        public PdfController(IWebHostEnvironment hostingEnvironment, IPdfDocumentRepository pdfDocumentRepository)
        {
            _pdfDocumentRepository = pdfDocumentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
       

        [HttpGet("search-pdf")]
        public async Task<IActionResult> Search(string query)
        {
            var searchParams = new SearchParams { Query = query };
            var documents = await _pdfDocumentRepository.SearchByParamsAsync(searchParams);
            return Ok(documents);
        }

        [HttpGet("download-pdf")]
        public IActionResult DownloadPdf(string fileName)
        {
            var filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "pdfs", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File {fileName} not found.");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/pdf", System.IO.Path.GetFileName(filePath));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile file, [FromForm] string author, [FromForm] string publisher)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { success = false, message = "No file provided." });
            }

            // Получение безопасного имени файла
            var fileName = Path.GetFileName(file.FileName);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest(new { success = false, message = "Invalid file name." });
            }

            // Построение пути для сохранения файла
            var path = Path.Combine(_hostingEnvironment.WebRootPath,  "pdfs", fileName);
            if (string.IsNullOrEmpty(_hostingEnvironment.WebRootPath) || string.IsNullOrEmpty(fileName))
            {
                return BadRequest(new { success = false, message = "Invalid file path or name." });
            }

            // Сохранение файла
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var text = ExtractTextFromPdf(path);
            var document = new Domain.PdfDocument
            {
                Title = fileName,
                Author = author,
                Publisher = publisher,
                Content = text,
                FilePath = path
            };

            await _pdfDocumentRepository.AddAsync(document);

            return Ok(new { success = true, message = "File uploaded successfully." });
        }
        private string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
    }
}