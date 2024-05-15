﻿using Microsoft.AspNetCore.Mvc;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PIMS.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using PIMS.Application;
using Path = System.IO.Path;
using iTextSharp.text;
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
        public async Task<IActionResult> Search([FromQuery]SearchParams searchParams)
        {
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
        public async Task<IActionResult> UploadPdf(IFormFile file, [FromForm] string author, [FromForm] string publisher, [FromForm] string keyWords, [FromForm] int year)
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

            // Чтение файла в массив байтов
            byte[] fileContent;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileContent = memoryStream.ToArray();
            }

            // Создание нового документа для сохранения в базе данных
            var document = new Domain.PdfDocument
            {
                Title = fileName,
                Author = author,
                Publisher = publisher,
                Keywords=keyWords,
                Year=year,
                Content = fileContent, // Теперь сохраняем массив байтов вместо пути файла
                Extension = Path.GetExtension(file.FileName) // Добавление расширения файла
            };

            // Добавление документа в базу данных
            await _pdfDocumentRepository.AddAsync(document);

            return Ok(new { success = true, message = "File uploaded successfully." });
        }
        [HttpPost("create-pdf")]
        public IActionResult CreatePdfFromData([FromBody] Domain.PdfDocument documentData)
        {
            if (documentData == null || documentData.Content == null || documentData.Content.Length == 0)
            {
                return BadRequest("Недостаточно данных для создания PDF.");
            }

            // Просто возвращаем существующий PDF, если Content уже содержит данные PDF
            return File(documentData.Content, "application/pdf", $"{documentData.Title}.pdf");
        }

    }
}