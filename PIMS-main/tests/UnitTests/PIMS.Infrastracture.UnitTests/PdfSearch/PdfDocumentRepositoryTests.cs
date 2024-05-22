using Microsoft.EntityFrameworkCore;
using Moq;
using PIMS.Application;
using PIMS.Domain;
using PIMS.Infrastructure.Persistence.DbContexts;
using PIMS.Infrastructure.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastracture.UnitTests.PdfSearch
{
    public class PdfDocumentRepositoryTests
    {
        private readonly Mock<PIMSDbContext> _mockContext;
        private readonly PdfDocumentRepository _repository;
        private readonly Mock<DbSet<PdfDocument>> _mockSet;
        private readonly List<PdfDocument> _pdfDocuments;

        public PdfDocumentRepositoryTests()
        {
            _mockContext = new Mock<PIMSDbContext>();
            _mockSet = new Mock<DbSet<PdfDocument>>();
            _repository = new PdfDocumentRepository(_mockContext.Object);

            _pdfDocuments = new List<PdfDocument>
        {
            new PdfDocument { Id = 1, Title = "Document1", Author = "Author1", Publisher = "Publisher1", Year = 2020, Keywords = "Keyword1", DocumentType = "Type1" },
            new PdfDocument { Id = 2, Title = "Document2", Author = "Author2", Publisher = "Publisher2", Year = 2021, Keywords = "Keyword2", DocumentType = "Type2" }
        };

           var mockSet = new Mock<DbSet<PdfDocument>>();
        mockSet.As<IQueryable<PdfDocument>>().Setup(m => m.Provider).Returns(_pdfDocuments.AsQueryable().Provider);
        mockSet.As<IQueryable<PdfDocument>>().Setup(m => m.Expression).Returns(_pdfDocuments.AsQueryable().Expression);
        mockSet.As<IQueryable<PdfDocument>>().Setup(m => m.ElementType).Returns(_pdfDocuments.AsQueryable().ElementType);
        mockSet.As<IQueryable<PdfDocument>>().Setup(m => m.GetEnumerator()).Returns(_pdfDocuments.GetEnumerator());

        _mockContext.Setup(c => c.PdfDocuments).Returns(mockSet.Object);

        _repository = new PdfDocumentRepository(_mockContext.Object);
        }

        [Fact]
        public async Task SearchByParamsAsync_ReturnsCorrectDocuments()
        {
            var searchParams = new SearchParams { Title = "Document1" };
            var documents = await _repository.SearchByParamsAsync(searchParams);

            Assert.Single(documents);
            Assert.Contains(documents, d => d.Title == "Document1");
        }

        [Fact]
        public async Task SearchByParamsAsync_NoParams_ReturnsAllDocuments()
        {
            var searchParams = new SearchParams { };
            var documents = await _repository.SearchByParamsAsync(searchParams);

            Assert.Equal(2, documents.Count);
        }

        // More tests can be added here to cover other filter conditions
    }
}
